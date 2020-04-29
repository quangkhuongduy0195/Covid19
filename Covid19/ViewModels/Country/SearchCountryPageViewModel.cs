using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Covid19.Services.Interfaces.Home;
using Prism.Navigation;

namespace Covid19.ViewModels.Country
{
    public class SearchCountryPageViewModel : ViewModelBase
    {
        readonly IRestService _restService;
        public SearchCountryPageViewModel(INavigationService navigationService, IRestService restService) : base(navigationService)
        {
            _restService = restService;
        }
        ObservableCollection<Models.Country> _countries = new ObservableCollection<Models.Country>();
        public ObservableCollection<Models.Country> Countries
        {
            get { return _countries; }
            set { SetProperty(ref _countries, value); }
        }
        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            var modeNav = parameters.GetNavigationMode();
            if(modeNav != NavigationMode.Back)
            {

                await GetAllCountries();
            }
        }

        async Task GetAllCountries()
        {
            var reponse = await _restService.GetAllCountries();
            Countries = new ObservableCollection<Models.Country>(reponse);
        }
    }
}
