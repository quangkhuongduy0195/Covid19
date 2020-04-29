using System;
using System.Threading.Tasks;
using Prism.Services;

namespace Covid19.Infrastructure.Networking.Refit
{
    internal class DialogSynchronizer
    {
        private DialogSynchronizer()
        {
            // hiden ctor
        }

        private static readonly Lazy<DialogSynchronizer> Lazy = new Lazy<DialogSynchronizer>(() => new DialogSynchronizer());
        /// <summary>
        /// Gets the singleton instance.
        /// </summary>
        /// <value>The instance.</value>
        public static DialogSynchronizer Instance => Lazy.Value;

        public bool IsConfirming { get; private set; }

        private Task<bool> _confirmingTask;
        /// <summary>
        /// Confirms the on main thread.
        /// </summary>
        /// <returns>The on main thread.</returns>
        public async Task<bool> ConfirmOnMainThreadAsync(string title, string message, string acceptButton, string cancelButton)
        {
            try
            {
                if (_confirmingTask != null && !_confirmingTask.IsCompleted)
                    return await _confirmingTask;
                IsConfirming = true;
                var pageDialogService = Ioc.Ioc.Current.Resolve<IPageDialogService>();
                _confirmingTask = Xamarin.Essentials.MainThread.InvokeOnMainThreadAsync(() => pageDialogService.DisplayAlertAsync(title, message, acceptButton, cancelButton)); ;
                var sure = await _confirmingTask;
                IsConfirming = false;
                return sure;
            }
            finally
            {
                _confirmingTask?.Dispose();
            }
        }
    }
}