using System;
using System.Threading.Tasks;
using Covid19.Infrastructure.Networking.Bases;

namespace Covid19.Infrastructure.Networking.Refit
{
    public interface IRest<TApi>
    {
        /// <summary>
        /// Do api call with retry and error handling
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="actionFactor"></param>
        /// <param name="retryMode"></param>
        /// <returns></returns>
        Task<TResult> CallWithRetry<TResult>(Func<TApi, Task<TResult>> actionFactor, RetryMode retryMode = RetryMode.Confirm)
            where TResult : ResponseBase, new();
    }
}