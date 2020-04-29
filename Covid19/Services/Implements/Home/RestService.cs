using System;
using Flurl;
using Flurl.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Covid19.Helpers;
using Covid19.Models;
using Covid19.Services.Interfaces.Home;

namespace Covid19.Services.Implements.Home
{
    public class RestService : IRestService
    {
        public async Task<IEnumerable<Country>> GetAllCountries()
        {
            try
            {
                var response = await Constants.BASE_URL
                    .AppendPathSegment("countries")
                    .WithTimeout(TimeSpan.FromSeconds(30))
                    .GetJsonAsync<IList<Country>>();
                return response;
            }
            catch (FlurlHttpException ex)
            {
                var error = ex.Message;
                return null;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return null;
            }
        }

        public async Task<GlobalTotals> GetGlobalTotals()
        {

            try
            {
                var response = await Constants.BASE_URL
                    .AppendPathSegment("all")
                    .WithTimeout(TimeSpan.FromSeconds(30))
                    .GetJsonAsync<GlobalTotals>();

                return response;
            }
            catch (FlurlHttpException ex)
            {
                var error = ex.Message;
                return null;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return null;
            }
        }

        public async Task<Country> GetTotalsByCountry(string countryISO)
        {
            try
            {
                var response = await Constants.BASE_URL
                    .AppendPathSegment($"countries/{countryISO}")
                    .WithTimeout(TimeSpan.FromSeconds(30))
                    .GetJsonAsync<Country>();

                return response;
            }
            catch (FlurlHttpException ex)
            {
                var error = ex.Message;
                return null;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return null;
            }
        }
    }
}
