using System;
using System.Globalization;
using Xamarin.Forms;

namespace Covid19.Converters
{
    public class ProvinceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            return string.IsNullOrWhiteSpace(value.ToString()) ? "" : " - Province: ";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
