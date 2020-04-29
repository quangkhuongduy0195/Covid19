using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Covid19.Services.Interfaces.Calendar;
using Xamarin.Plugin.Calendar.Models;

namespace Covid19.Services.Implements.Calendar
{
    public class CalendarService : ICalendar
    {
        public async Task<EventCollection> GetDataReport(CancellationToken token)
        {
            await Task.Delay(3000);
            var events = new EventCollection
            {
                [DateTime.Now] = new List<EventModel>
                    {
                        new EventModel { Name = "Cool event1", Description = "This is Cool event1's description!" },
                        new EventModel { Name = "Cool event2", Description = "This is Cool event2's description!" },
                        new EventModel { Name = "Cool event1", Description = "This is Cool event1's description!" },
                        new EventModel { Name = "Cool event2", Description = "This is Cool event2's description!" },
                        new EventModel { Name = "Cool event1", Description = "This is Cool event1's description!" },
                        new EventModel { Name = "Cool event2", Description = "This is Cool event2's description!" },
                        new EventModel { Name = "Cool event1", Description = "This is Cool event1's description!" },
                        new EventModel { Name = "Cool event2", Description = "This is Cool event2's description!" },
                        new EventModel { Name = "Cool event1", Description = "This is Cool event1's description!" },
                        new EventModel { Name = "Cool event2", Description = "This is Cool event2's description!" },
                        new EventModel { Name = "Cool event1", Description = "This is Cool event1's description!" },
                        new EventModel { Name = "Cool event2", Description = "This is Cool event2's description!" },
                        new EventModel { Name = "Cool event1", Description = "This is Cool event1's description!" },
                        new EventModel { Name = "Cool event2", Description = "This is Cool event2's description!" },
                        new EventModel { Name = "Cool event1", Description = "This is Cool event1's description!" },
                        new EventModel { Name = "Cool event2", Description = "This is Cool event2's description!" },
                        new EventModel { Name = "Cool event1", Description = "This is Cool event1's description!" },
                        new EventModel { Name = "Cool event2", Description = "This is Cool event2's description!" },
                        new EventModel { Name = "Cool event1", Description = "This is Cool event1's description!" },
                        new EventModel { Name = "Cool event2", Description = "This is Cool event2's description!" },
                        new EventModel { Name = "Cool event1", Description = "This is Cool event1's description!" },
                        new EventModel { Name = "Cool event2", Description = "This is Cool event2's description!" },
                        new EventModel { Name = "Cool event1", Description = "This is Cool event1's description!" },
                        new EventModel { Name = "Cool event2", Description = "This is Cool event2's description!" },
                        new EventModel { Name = "Cool event1", Description = "This is Cool event1's description!" },
                        new EventModel { Name = "Cool event2", Description = "This is Cool event2's description!" },
                        new EventModel { Name = "Cool event1", Description = "This is Cool event1's description!" },
                        new EventModel { Name = "Cool event2", Description = "This is Cool event2's description!" }
                    }
            };
            return events;
        }

        public class EventModel
        {
            //public EventModel(string name, string description)
            //{
            //    Name = name;
            //    Description = description;
            //}

            public string Name { get; set; }
            public string Description { get; set; }

        }
    }
}
