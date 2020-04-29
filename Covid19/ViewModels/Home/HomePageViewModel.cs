using System;
using System.Threading.Tasks;
using Covid19.Extensions;
using Covid19.Helpers;
using Covid19.Services.Implements.Home;
using Covid19.Services.Interfaces.Home;
using Covid19.Styles;
using Covid19.ViewModels.Menu;
using Covid19.Views;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace Covid19.ViewModels.Home
{
    public class HomePageViewModel : ViewModelBase
    {
        #region BindingProperty
        public bool _busyGlobal = false;
        public bool BusyGlobal
        {
            get { return _busyGlobal; }
            set { SetProperty(ref _busyGlobal, value); }
        }

        public bool _busyCoutry = false;
        public bool BusyCoutry
        {
            get { return _busyCoutry; }
            set { SetProperty(ref _busyCoutry, value); }
        }

        public string _globalConfirmed = "-";
        public string GlobalConfirmed
        {
            get { return _globalConfirmed; }
            set { SetProperty(ref _globalConfirmed, value); }
        }

        public string _globalDeaths = "-";
        public string GlobalDeaths
        {
            get { return _globalDeaths; }
            set { SetProperty(ref _globalDeaths, value); }
        }

        public string _globalRecovered = "-";
        public string GlobalRecovered
        {
            get { return _globalRecovered; }
            set { SetProperty(ref _globalRecovered, value); }
        }

        public string _countryConfirmed = "-";
        public string CountryConfirmed
        {
            get { return _countryConfirmed; }
            set { SetProperty(ref _countryConfirmed, value); }
        }

        public string _countryDeaths = "-";
        public string CountryDeaths
        {
            get { return _countryDeaths; }
            set { SetProperty(ref _countryDeaths, value); }
        }

        public string _countryRecovered = "-";
        public string CountryRecovered
        {
            get { return _countryRecovered; }
            set { SetProperty(ref _countryRecovered, value); }
        }

        public string _titleMainHeader = "-";
        public string TitleMainHeader
        {
            get { return _titleMainHeader; }
            set { SetProperty(ref _titleMainHeader, value); }
        }

        public string _lastUpdateMainHeader = "-";
        public string LastUpdateMainHeader
        {
            get { return _lastUpdateMainHeader; }
            set { SetProperty(ref _lastUpdateMainHeader, value); }
        }

        public string _titleGlobal = "-";
        public string TitleGlobal
        {
            get { return _titleGlobal; }
            set { SetProperty(ref _titleGlobal, value); }
        }

        public string _titleConfirmed = "-";
        public string TitleConfirmed
        {
            get { return _titleConfirmed; }
            set { SetProperty(ref _titleConfirmed, value); }
        }

        public string _titleRecovered = "-";
        public string TitleRecovered
        {
            get { return _titleRecovered; }
            set { SetProperty(ref _titleRecovered, value); }
        }

        public string _titleDeaths = "-";
        public string TitleDeaths
        {
            get { return _titleDeaths; }
            set { SetProperty(ref _titleDeaths, value); }
        }

        public string _lastUpdateGlobal = "-";
        public string LastUpdateGlobal
        {
            get { return _lastUpdateGlobal; }
            set { SetProperty(ref _lastUpdateGlobal, value); }
        }

        public string _titleCountry = "-";
        public string TitleCountry
        {
            get { return _titleCountry; }
            set { SetProperty(ref _titleCountry, value); }
        }

        public string _lastUpdateCountry = "-";
        public string LastUpdateCountry
        {
            get { return _lastUpdateCountry; }
            set { SetProperty(ref _lastUpdateCountry, value); }
        }

        public string _refreshHeader = "-";
        public string RefreshHeader
        {
            get { return _refreshHeader; }
            set { SetProperty(ref _refreshHeader, value); }
        }

        #endregion

        #region Command
        DelegateCommand _globalRefreshCommand;
        public DelegateCommand GlobalRefreshCommand => _globalRefreshCommand ?? (_globalRefreshCommand = new DelegateCommand(async () =>
        {
            await GetDataGlobal();
        }));

        DelegateCommand _countryRefreshCommand;
        public DelegateCommand CountryRefreshCommand => _countryRefreshCommand ?? (_countryRefreshCommand = new DelegateCommand(async () =>
        {
            await GetDataCountry(_countryISO);
        }));

        DelegateCommand _selectCountryCommand;
        public DelegateCommand SelectCountryCommand => _selectCountryCommand ?? (_selectCountryCommand = new DelegateCommand(async () =>
        {
            await _navigationService.NavigateAsync($"{nameof(SearchCountryPage)}");
        }));

        DelegateCommand _menuCommand;
        public DelegateCommand MenuCommand => _menuCommand ?? (_menuCommand = new DelegateCommand(async () =>
        {
            await _navigationService.NavigateAsync($"{nameof(MenuPage)}");
        }));
        #endregion

        #region Variable
        readonly IRestService _restService;
        string _countryISO = "VN";
        Models.Country _country;
        INavigationService _navigationService;
        #endregion

        public HomePageViewModel(INavigationService navigationService, IRestService restService) : base(navigationService)
        {
            _navigationService = navigationService;
            _restService = restService;
            SetDarkTheme(Xamarin.Essentials.Preferences.Get("appDarkTheme", false));
            MessagingCenter.Subscribe<MenuPageViewModel, bool>(this, "ChangeLanguade", (obj, val) =>
            {
                ChangeLanguage();
            });
            ChangeLanguage();
        }

        public override void Destroy()
        {
            base.Destroy();
            MessagingCenter.Unsubscribe<MenuPageViewModel, bool>(this, "ChangeLanguade");
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            var navigationMode = parameters.GetNavigationMode();
            if (navigationMode != NavigationMode.Back)
            {           
                BusyCoutry = true;
                BusyGlobal = true;
                await GetDataGlobal();
                await GetDataCountry(_countryISO);
                ChangeLanguage();
            }
        }

        async Task GetDataGlobal()
        {
            BusyGlobal = true;
            var reponse = await _restService.GetGlobalTotals();
            SetTotalGlobal(reponse);
            BusyGlobal = false;
        }

        void SetTotalGlobal(Models.GlobalTotals globalTotals)
        {
            GlobalConfirmed = (globalTotals?.cases ?? 0).TransformNumberToString();
            GlobalRecovered = (globalTotals?.recovered ?? 0).TransformNumberToString();
            GlobalDeaths = (globalTotals?.deaths ?? 0).TransformNumberToString();
        }

        async Task GetDataCountry(string countryISO)
        {
            BusyCoutry = true;
            var reponse = await _restService.GetTotalsByCountry(countryISO);
            SetTotalCountry(reponse);
            _country = reponse;
            BusyCoutry = false;
        }

        void SetTotalCountry(Models.Country globalTotals)
        {
            CountryConfirmed = (globalTotals?.cases ?? 0).TransformNumberToString();
            CountryRecovered = (globalTotals?.recovered ?? 0).TransformNumberToString();
            CountryDeaths = (globalTotals?.deaths ?? 0).TransformNumberToString();
        }

        void ChangeLanguage()
        {
            TitleMainHeader = TranslateExtension.TranslateText("MainHeader");

            TitleGlobal = TranslateExtension.TranslateText("GlobalUpdateHeader");
            TitleConfirmed = TranslateExtension.TranslateText("ConfirmedHeader");
            TitleRecovered = TranslateExtension.TranslateText("RecoveredHeader");
            TitleDeaths = TranslateExtension.TranslateText("DeathsHeader");
            RefreshHeader = TranslateExtension.TranslateText("RefreshHeader");
            TitleCountry = TranslateExtension.TranslateText(_country?.country ?? null);
        }
    }
}
