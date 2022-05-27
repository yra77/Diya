
using SkiaSharp.Views.Forms;
using System;
using System.Globalization;
using Xamarin.Forms;


namespace MyApp.Helpers
{
    class SKTouchEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var SKTouch_EventArgs = value as SKTouchEventArgs;
            if (SKTouch_EventArgs == null)
            {
                throw new ArgumentException("Expected value to be of type SKPaintSurfaceEventArgs", nameof(value));
            }
            return SKTouch_EventArgs;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
