using System;
using Covid19.Controls.ExtendedElements;
using Covid19.Infrastructure.Networking.Ioc;
using Covid19.ViewModels.Calendar;
using Covid19.ViewModels.Corona;
using Covid19.ViewModels.Country;
using Covid19.ViewModels.Home;
using Covid19.ViewModels.Menu;
using Covid19.Views.Calendar;
using Prism.Ioc;
using Xamarin.Forms;

namespace Covid19.Views
{
    public class NavigationRegistry : IIocRegistry
    {
        public void Register(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>(nameof(NavigationPage));
            containerRegistry.RegisterForNavigation<BottomTabbedPage>(nameof(BottomTabbedPage));
            containerRegistry.RegisterForNavigation<CoronaPage, CoronaPageViewModel>(nameof(CoronaPage));
            containerRegistry.RegisterForNavigation<CalendarPage, CalendarPageViewModel>(nameof(CalendarPage));
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>(nameof(HomePage));
            containerRegistry.RegisterForNavigation<MenuPage, MenuPageViewModel>(nameof(MenuPage));
            containerRegistry.RegisterForNavigation<SearchCountryPage, SearchCountryPageViewModel>(nameof(SearchCountryPage));
        }
    }
}
