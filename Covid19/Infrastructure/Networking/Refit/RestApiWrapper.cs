using System;
using System.Net.Http;
using System.Threading.Tasks;
using Covid19.Infrastructure.Networking.Bases;
using Covid19.Services.Interfaces;
using Refit;

namespace Covid19.Infrastructure.Networking.Refit
{
    internal class RestApiWrapper<TApi> : IRest<TApi>
    {
        private const int TimeoutInSeconds = 10;
        private readonly TApi _api;
        private readonly ILogService _logger;
        public RestApiWrapper(ILogService logger)
        {
            _api = Build<TApi>();
            _logger = logger;
        }

        public async Task<TResult> CallWithRetry<TResult>(Func<TApi, Task<TResult>> taskFunction, RetryMode retryMode = RetryMode.Confirm)
            where TResult : ResponseBase, new()
        {
            var callWithConfirmationTask = default(Task<TResult>);
            try
            {
                if (taskFunction == null)
                    throw new ArgumentNullException(nameof(taskFunction));
                switch (retryMode)
                {
                    case RetryMode.Confirm:
                        callWithConfirmationTask = CallWithConfirmation(taskFunction);
                        var rs = await callWithConfirmationTask;
                        return rs;
                    default:
                        /* ==================================================================================================
                         * the retry mode is not supported yet!
                         * ================================================================================================*/
                        throw new ArgumentOutOfRangeException(nameof(retryMode), $"The retry mode '{retryMode}' is not supported yet!");
                }
            }
            finally
            {
                callWithConfirmationTask?.Dispose();
            }
        }

        #region private methods, executor

        private async Task<T> CallWithConfirmation<T>(Func<TApi, Task<T>> taskFunction) where T : ResponseBase, new()
        {
            var result = default(T);
            var toBeContinued = false;
            var actionTask = default(Task<T>);
            do
            {
                try
                {
                    if (toBeContinued)
                    {
                        var retry = await DialogSynchronizer.Instance.ConfirmOnMainThreadAsync("confirm", "Can not connect to server, do you want to retry?", "Retry", "Cancel"); // todo: match spec
                        if (!retry)
                        {
                            break;
                        }
                    }
                    actionTask = ActionSendAsync(taskFunction);
                    result = await actionTask;
                    result.PhysicalResult = PhysicalResult.Succeeded;
                    toBeContinued = false;
                }
                catch (OperationCanceledException ex)
                {
                    _logger.Log(ex);
                    result = new T()
                    {
                        PhysicalResult = PhysicalResult.Canceled
                    };
                    toBeContinued = false;
                }
                catch (Exception ex)
                {
                    _logger.Log(ex);
                    result = new T()
                    {
                        PhysicalResult = PhysicalResult.Failed
                    };
                    toBeContinued = true;
                }
                finally
                {
                    actionTask?.Dispose();
                }
            } while (toBeContinued);
            return result;
        }

        /* ==================================================================================================
         * Why use Func<Task<T>> instead of an implicit task?
         * => 
         * In case of the api task failed by network connection lost, its still has faulted status forever.
         * Use an Func<> to retrieve a new task for each retry
         * ================================================================================================*/
        async Task<T> ActionSendAsync<T>(Func<TApi, Task<T>> taskFunction)
        {
            Task<T> task = default;
            try
            {
                task = taskFunction(_api);
                /* ==================================================================================================
                 * retrieve the api task from task factory
                 * ================================================================================================*/
                var mainResult = await task;
                return mainResult;
            }
            finally
            {
                task?.Dispose();
            }
        }
        #endregion

        private T Build<T>()
        {
            /* ==================================================================================================
             * set default refit setting
             * ie: ignore null field from json string
             * ================================================================================================*/
            var defaultSettings = new RefitSettings
            {
                ContentSerializer = new JsonContentSerializer(new Newtonsoft.Json.JsonSerializerSettings
                {
                    NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                })
            };
            return RestService.For<T>(Client, defaultSettings);
        }

        private static readonly HttpClient Client = GetHttpClient(); // use as singleton
        private static HttpClient GetHttpClient()
        {
            /* ==================================================================================================
             * Use native handler for better perfomance
             * For Android v4.4 vs ModernHttp compability => Work as expected on this version
             * ================================================================================================*/
            var handler = new ExtendedNativeMessageHandler(TimeoutInSeconds)
            {
                DisableCaching = true,
                AllowAutoRedirect = false
                /* ==================================================================================================
                 * The property "MessageHandler.TimeOut" is not reliable
                 * => use CancellationToken instead
                 * ================================================================================================*/
            };

            var toReturn = new HttpClient(handler)
            {
                BaseAddress = new Uri(ApiHost.MainHost),
                /* ==================================================================================================
                 * The property "HttpClient.TimeOut" is not reliable
                 * => use CancellationToken instead
                 * ================================================================================================*/
            };
            return toReturn;
        }
    }
}
