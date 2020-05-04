using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Covid19.Services.Interfaces.Home;
using Prism.Commands;
using Prism.Navigation;

namespace Covid19.ViewModels.Country
{
    public class SelectCountryPageViewModel : ViewModelBase
    {
        List<Models.Country> _tmpCountries;
        List<Models.Country> _tmpCountriesUI = new List<Models.Country>();

        ObservableCollection<Models.Country> _countries = new ObservableCollection<Models.Country>();
        public ObservableCollection<Models.Country> Countries
        {
            get { return _countries; }
            set { SetProperty(ref _countries, value); }
        }

        private Models.Country _countrySelected;
        public Models.Country CountrySelected
        {
            get { return _countrySelected; }
            set { SetProperty(ref _countrySelected, value); }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        private bool _isBusyLoadMore;
        public bool IsBusyLoadMore
        {
            get { return _isBusyLoadMore; }
            set { SetProperty(ref _isBusyLoadMore, value); }
        }

        private int _itemTreshold = 5;
        public int ItemTreshold
        {
            get { return _itemTreshold; }
            set { SetProperty(ref _itemTreshold, value); }
        }

        private int _totalCountries;
        public int TotalCountries
        {
            get { return _totalCountries; }
            set { SetProperty(ref _totalCountries, value); }
        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                SearchCountry(value);
                SetProperty(ref _searchText, value);
            }
        }


        DelegateCommand _closeCommand { get; set; }
        public DelegateCommand CloseCommand => _closeCommand ?? (_closeCommand = new DelegateCommand(async () =>
        {
            await _navigationService.GoBackAsync();
        }));

        DelegateCommand<Models.Country> _countrySelectedCommand { get; set; }
        public DelegateCommand<Models.Country> CountrySelectedCommand => _countrySelectedCommand ?? (_countrySelectedCommand = new DelegateCommand<Models.Country>(async (country) =>
        {
            var parameters = new NavigationParameters();
            parameters.Add("country", country);

            await _navigationService.GoBackAsync(parameters);
        }));

        DelegateCommand _itemTresholdReachedCommand { get; set; }
        public DelegateCommand ItemTresholdReachedCommand => _itemTresholdReachedCommand ?? (_itemTresholdReachedCommand = new DelegateCommand(async () =>
        {
            LoadMoreData();
        }));

        readonly IRestService _restService;
        int _currentPage = 1;
        readonly INavigationService _navigationService;

        public SelectCountryPageViewModel(INavigationService navigationService, IRestService restService) : base(navigationService)
        {
            _restService = restService;
            _navigationService = navigationService;
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            var modeNav = parameters.GetNavigationMode();
            if (modeNav != NavigationMode.Back)
            {
                await GetAllCountries();
            }
        }

        async Task GetAllCountries()
        {
            _currentPage = 1;
            IsBusy = true;
            var reponse = await _restService.GetAllCountries();
            if (reponse != null)
            {
                _tmpCountries = new List<Models.Country>(reponse);
                TotalCountries = _tmpCountries.Count();
                SetDataonListUI(page: _currentPage);
            }
            IsBusy = false;
        }

        void SetDataonListUI(int page = 1)
        {
            int start = (page * 20) - 20;
            int end = (page * 20);
            end = end < _tmpCountries.Count ? end : _tmpCountries.Count;
            for (int i = start; i < end; i++)
            {
                Countries.Add(_tmpCountries[i]);
                _tmpCountriesUI.Add(_tmpCountries[i]);
            }
        }

        void LoadMoreData()
        {
            if (IsBusyLoadMore)
                return;
            IsBusyLoadMore = true;
            ItemTreshold = -1;
            _currentPage++;
            SetDataonListUI(page: _currentPage);
            ItemTreshold = _totalCountries == Countries.Count ? -1 : 5;
            IsBusyLoadMore = false;
        }

        void SearchCountry(string text)
        {
            Countries.Clear();
            if (string.IsNullOrEmpty(text))
            {
                foreach (var item in _tmpCountriesUI)
                {
                    Countries.Add(item);
                }
            }
            else
            {
                IEnumerable<Models.Country> results = _tmpCountries.Where(s => s.country.Contains(text));
                foreach (var item in results)
                {
                    Countries.Add(item);
                }
            }
        }
    }
}
