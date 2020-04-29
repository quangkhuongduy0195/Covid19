using System;
using Covid19.Extensions;
using Covid19.Infrastructure.Networking.Ioc;
using Covid19.ViewModels.Corona;
using Covid19.Views;
using Plugin.Multilingual;
using Prism;
using Prism.Ioc;
using Prism.Plugin.Popups;
using Prism.Unity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Covid19
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer platformInitializer = null) : base(platformInitializer) { }

        protected async override void OnInitialized()
        {
            InitializeComponent();
            CrossMultilingual.Current.CurrentCultureInfo = new System.Globalization.CultureInfo("vi");
            AppResources.Culture = CrossMultilingual.Current.CurrentCultureInfo;
            await NavigationService.GoToMainPageAsync();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            Ioc.Current.RegisterTypes(containerRegistry, new ServicesRegistry(), new NavigationRegistry());
            containerRegistry.RegisterPopupNavigationService();
        }
    }
}
