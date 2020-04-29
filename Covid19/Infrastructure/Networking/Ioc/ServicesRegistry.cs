using System;
using Covid19.Infrastructure.Networking.Refit;
using Covid19.Services;
using Covid19.Services.Corona;
using Covid19.Services.Implements.Calendar;
using Covid19.Services.Implements.Home;
using Covid19.Services.Interfaces;
using Covid19.Services.Interfaces.Calendar;
using Covid19.Services.Interfaces.Corona;
using Covid19.Services.Interfaces.Home;
using Prism.Ioc;

namespace Covid19.Infrastructure.Networking.Ioc
{
    public class ServicesRegistry : IIocRegistry
    {
        public void Register(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ILogService, LogService>();
            containerRegistry.Register(typeof(IRest<>), typeof(RestApiWrapper<>));
            containerRegistry.Register<ICovidService, CovidService>();
            containerRegistry.Register<ICalendar, CalendarService>();
            containerRegistry.Register<IRestService, RestService>();
        }
    }
}
