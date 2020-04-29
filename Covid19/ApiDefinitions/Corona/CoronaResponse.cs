using System;
using Covid19.Infrastructure.Networking.Bases;
using Covid19.Models.Corona;
using Newtonsoft.Json;

namespace Covid19.ApiDefinitions.Corona
{
    public class CoronaResponse : ResponseBase
    {
        [JsonProperty("data")]
        public CovidData ListCovidData { get; set; }
    }
}
