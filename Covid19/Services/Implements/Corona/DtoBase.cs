using System;
using Covid19.Infrastructure.Networking.Bases;

namespace Covid19.Services.Corona
{
    public class DtoBase
    {
        public DtoBase(ResponseBase response)
        {
            Rrror = response.Rrror;
            StatusCode = response.StatusCode;
            Message = response.Message;
            PhysicalResult = response.PhysicalResult;
        }

        public bool Rrror { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public PhysicalResult PhysicalResult { get; set; }
    }
}
