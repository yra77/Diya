
using MyApp.Events;
using System;
using System.Globalization;
using Xamarin.Forms;


namespace MyApp.Helpers
{
    public class TouchActionEventArgs_Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var Touch_EventArgs = value as TouchActionEventArgs;
            if (Touch_EventArgs == null)
            {
                throw new ArgumentException("Expected value to be of type EventArgs", nameof(value));
            }
            return Touch_EventArgs;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
