using System;
using System.Threading;
using System.Threading.Tasks;
using Refit;

namespace Covid19.ApiDefinitions.Corona
{
    [Headers("x-rapidapi-host: covid-19-coronavirus-statistics.p.rapidapi.com", "x-rapidapi-key: e04363bb09msh9635ed25a86b1f2p189847jsn551461d2e401")]
    public interface CoronaApi
    {
        [Get("/stats?country=Vietnam")]
        Task<CoronaResponse> GetDataCovidWorld(CancellationToken cancellationToken);
    }
}
