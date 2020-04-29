using System;
using System.Linq;
using Covid19.Helpers;
using Covid19.Styles;
using Plugin.Multilingual;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Covid19.ViewModels.Menu
{
    public class MenuPageViewModel : ViewModelBase
    {
        bool _appDarkTheme;
        public bool AppDarkTheme
        {
            get { return _appDarkTheme; }
            set
            {
                SetDarkTheme(value);
                Preferences.Set("appDarkTheme", value);
                SetProperty(ref _appDarkTheme, value);
            }
        }

        bool _appLanguageVI;
        public bool AppLanguageVI
        {
            get { return _appLanguageVI; }
            set { SetProperty(ref _appLanguageVI, value); }
        }

        bool _appLanguageEN;
        public bool AppLanguageEN
        {
            get { return _appLanguageEN; }
            set { SetProperty(ref _appLanguageEN, value); }
        }

        bool _appLanguagePT;
        public bool AppLanguagePT
        {
            get { return _appLanguagePT; }
            set { SetProperty(ref _appLanguagePT, value); }
        }

        #region Command
        DelegateCommand _enLanguageCommand;
        public DelegateCommand EnLanguageCommand => _enLanguageCommand ?? (_enLanguageCommand = new DelegateCommand(async () =>
        {
            ChangeLanguage(name: "en");
        }));

        DelegateCommand _brLanguageCommand;
        public DelegateCommand BrLanguageCommand => _brLanguageCommand ?? (_brLanguageCommand = new DelegateCommand(async () =>
        {
            ChangeLanguage(name: "pt");
        }));

        DelegateCommand _vnLanguageCommand;
        public DelegateCommand VnLanguageCommand => _vnLanguageCommand ?? (_vnLanguageCommand = new DelegateCommand(async () =>
        {
            ChangeLanguage(name: "vi");
        }));
        #endregion


        public MenuPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            AppDarkTheme = Preferences.Get("appDarkTheme", false);
            SetLanguage(Preferences.Get("appLanguage", "vi"));
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            var modeNav = parameters.GetNavigationMode();
        }

        void ChangeLanguage(string name)
        {
            CrossMultilingual.Current.CurrentCultureInfo = new System.Globalization.CultureInfo(name);
            AppResources.Culture = CrossMultilingual.Current.CurrentCultureInfo;
            MessagingCenter.Send(this, "ChangeLanguade", true);
            Preferences.Get("appLanguage", name);
            SetLanguage(name);
        }

        void SetLanguage(string name)
        {
            switch (name)
            {
                case "vi":
                    AppLanguageVI = true;
                    AppLanguageEN = AppLanguagePT = false;
                    break;
                case "en":
                    AppLanguageEN = true;
                    AppLanguageVI = AppLanguagePT = false;
                    break;
                case "pt":
                    AppLanguagePT = true;
                    AppLanguageEN = AppLanguageVI = false;
                    break;
            }
        }
    }
}
