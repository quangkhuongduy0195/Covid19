using System;
using System.Threading;
using System.Threading.Tasks;

namespace Covid19.Services.Interfaces.Corona
{
    public interface ICovidService
    {
        Task<CoronaDto> GetCorona(CancellationToken token);
    }
}
