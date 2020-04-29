using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Covid19.Models;

namespace Covid19.Services.Interfaces.Home
{
    public interface IRestService
    {
        Task<GlobalTotals> GetGlobalTotals();
        Task<Country> GetTotalsByCountry(string countryISO);
        Task<IEnumerable<Country>> GetAllCountries();
    }
}
