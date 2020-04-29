using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Covid19.Models.Corona
{
    public class CovidData
    {
        [JsonProperty("lastChecked")]
        public DateTime LastChecked { get; set; }

        [JsonProperty("covid19Stats")]
        public IList<Covid19Stats> Covid19Stats { get; set; }
    }
}
