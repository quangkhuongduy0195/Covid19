using System;
namespace Covid19.Infrastructure.Networking.Bases
{
    public enum PhysicalResult
    {
        /// <summary>
        /// api call failed
        /// </summary>
        Failed,
        /// <summary>
        /// that mean recieved response from server.
        /// </summary>
        Succeeded,
        /// <summary>
        /// the api call was canceled
        /// </summary>
        Canceled
    }
}
