using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Covid19.ApiDefinitions.Corona;
using Covid19.Models.Corona;
using Covid19.Services.Corona;

namespace Covid19.Services.Interfaces.Corona
{
    public class CoronaDto : DtoBase
    {
        public DateTime LastChecked { get; set; }
        public ObservableCollection<Covid19Stats> Covid19s { get; set; }

        public CoronaDto(CoronaResponse response) : base(response)
        {
            if (response != null)
            {
                LastChecked = response.ListCovidData.LastChecked;
                Covid19s = new ObservableCollection<Covid19Stats>(response.ListCovidData.Covid19Stats);
            }
        }
    }
}
