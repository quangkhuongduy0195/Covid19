using System;
using System.Threading;
using System.Threading.Tasks;
using Covid19.ApiDefinitions.Corona;
using Covid19.Infrastructure.Networking.Refit;
using Covid19.Services.Interfaces.Corona;

namespace Covid19.Services.Corona
{
    public class CovidService : ICovidService
    {
        private readonly IRest<CoronaApi> _coronaRest;

        public CovidService(IRest<CoronaApi> coronaRest)
        {
            _coronaRest = coronaRest;
        }

        public async Task<CoronaDto> GetCorona(CancellationToken token)
        {
            var repose = await _coronaRest.CallWithRetry(api => api.GetDataCovidWorld(token));
            var dto = new CoronaDto(repose);
            return dto;
        }
    }
}
