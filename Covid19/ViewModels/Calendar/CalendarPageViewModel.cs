using System;
using System.Threading;
using System.Threading.Tasks;
using Covid19.Services.Interfaces.Calendar;
using Covid19.ViewModels.Base;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Plugin.Calendar.Models;

namespace Covid19.ViewModels.Calendar
{
    public class CalendarPageViewModel : TabbedChildViewModelBase
    {
        public DelegateCommand DayTappedCommand => new DelegateCommand(async () => await DayTapped());

        private DateTime _selectedDate = DateTime.Today;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }

        EventCollection _events = new EventCollection();
        public EventCollection Events
        {
            get => _events;
            set => SetProperty(ref _events, value);
        }
        ICalendar _calendar;
        public CalendarPageViewModel(INavigationService navigationService, ICalendar calendar) : base(navigationService)
        {
            _calendar = calendar;
        }
        public async override void RaiseIsActiveChanged()
        {
            base.RaiseIsActiveChanged();
            if(IsActive)
            {
                Events = await _calendar.GetDataReport(CancellationToken.None);
            }
        }

        private async Task DayTapped()
        {
            var message = $"Received tap event from date: {SelectedDate}";
            //await App.Current.MainPage.DisplayAlert("DayTapped", message, "Ok");
            Console.WriteLine(message);
        }
    }
}
