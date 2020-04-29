using System;
using System.Collections.Generic;
using System.Windows.Input;
using Covid19.ViewModels.Calendar;
using Xamarin.Forms;

namespace Covid19.Controls.ExtendedElements.Calendar
{
    public partial class CalenderEvent : ContentView
    {
        public static BindableProperty CalenderEventCommandProperty =
            BindableProperty.Create(nameof(CalenderEventCommand), typeof(ICommand), typeof(CalenderEvent), null);

        public CalenderEvent()
        {
            InitializeComponent();
        }

        public ICommand CalenderEventCommand
        {
            get => (ICommand)GetValue(CalenderEventCommandProperty);
            set => SetValue(CalenderEventCommandProperty, value);
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            if (BindingContext is CalendarPageViewModel eventModel)
            {
                CalenderEventCommand?.Execute(eventModel);
            }
        }
    }
}
