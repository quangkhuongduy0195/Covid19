using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ModernHttpClient;

namespace Covid19.Infrastructure.Networking.Bases
{
    public class ExtendedNativeMessageHandler : NativeMessageHandler
    {
        private readonly int _timeoutInSeconds;
        public ExtendedNativeMessageHandler(int timeoutInSeconds)
        {
            _timeoutInSeconds = timeoutInSeconds;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var timeoutCts = new CancellationTokenSource();
            timeoutCts.CancelAfter(TimeSpan.FromSeconds(_timeoutInSeconds));
            var combined = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, timeoutCts.Token);
            try
            {
                /* ==================================================================================================
                 * provide authorization bearer token if needed
                 * ================================================================================================*/
                request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var response = await base.SendAsync(request, combined.Token).ConfigureAwait(false);
                return response;
            }
            catch (TaskCanceledException ex)
            {
                if (timeoutCts.IsCancellationRequested)
                    throw new TimeoutException(); // redirect to correct exception
                throw ex;
            }
            finally
            {
                timeoutCts.Dispose();
            }
        }
    }
}
