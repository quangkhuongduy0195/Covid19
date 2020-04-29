using System;
using System.Threading.Tasks;
using Covid19.Controls.ExtendedElements;
using Covid19.Views;
using Covid19.Views.Calendar;
using Prism.Navigation;
using Xamarin.Forms;

namespace Covid19.Extensions
{
    public static class NavigationShortcutExtensions
    {
        public static Task<INavigationResult> GoToMainPageAsync(this INavigationService navigationService)
        {
            // todo: matching spec
            if (navigationService == null)
                throw new ArgumentNullException(nameof(navigationService));
            //return navigationService.NavigateAsync($"{nameof(BottomTabbedPage)}?{KnownNavigationParameters.CreateTab}={nameof(NavigationPage)}|{nameof(CoronaPage)}" +
            //                                       $"&{KnownNavigationParameters.CreateTab}={nameof(NavigationPage)}|{nameof(CalendarPage)}");
            return navigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(HomePage)}");
        }
    }
}
