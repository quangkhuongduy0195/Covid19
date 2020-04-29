using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using Covid19.Models.Corona;
using Covid19.Services.Interfaces.Corona;
using Covid19.ViewModels.Base;
using Prism.Navigation;

namespace Covid19.ViewModels.Corona
{
    public class CoronaPageViewModel : TabbedChildViewModelBase
    {
        private readonly ICovidService _covidService;

        ObservableCollection<Covid19Stats> _dataCovids = new ObservableCollection<Covid19Stats>();
        public ObservableCollection<Covid19Stats> DataCovids
        {
            get { return _dataCovids; }
            set { SetProperty(ref _dataCovids, value); }
        }
        public string _title = "Covid-19";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        public CoronaPageViewModel(INavigationService navigationService, ICovidService covidService) : base(navigationService)
        {
            _covidService = covidService;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }

        public async override void RaiseIsActiveChanged()
        {
            base.RaiseIsActiveChanged();
            if(IsActive)
            {
                var data = await _covidService.GetCorona(CancellationToken.None);
                if (data != null)
                {
                    DataCovids = data.Covid19s;
                }
            }
        }
    }
}
