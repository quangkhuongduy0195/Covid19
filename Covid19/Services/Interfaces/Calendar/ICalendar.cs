using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Plugin.Calendar.Models;

namespace Covid19.Services.Interfaces.Calendar
{
    public interface ICalendar
    {
        Task<EventCollection> GetDataReport(CancellationToken token);
    }
}
