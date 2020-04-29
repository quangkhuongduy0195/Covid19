using System;
using Prism;
using Prism.Navigation;

namespace Covid19.ViewModels.Base
{
    public class TabbedChildViewModelBase : ViewModelBase, IActiveAware
    {
        public TabbedChildViewModelBase(INavigationService navigationService) : base(navigationService)
        {
        }

        // NOTE: Prism.Forms only sets IsActive, and does not do anything with the event.
        public event EventHandler IsActiveChanged;

        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { SetProperty(ref _isActive, value, RaiseIsActiveChanged); }
        }

        /// <summary>
        /// Raises the is active changed.
        /// </summary>
        public virtual void RaiseIsActiveChanged()
        {
            IsActiveChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
