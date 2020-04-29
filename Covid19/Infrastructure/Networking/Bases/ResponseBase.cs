using System;
using Newtonsoft.Json;

namespace Covid19.Infrastructure.Networking.Bases
{
    public class ResponseBase
    {
        [JsonProperty("error")]
        public bool Rrror { get; set; }

        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonIgnore]
        public PhysicalResult PhysicalResult { get; set; }
    }
}
