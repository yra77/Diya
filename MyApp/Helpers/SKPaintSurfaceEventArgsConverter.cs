
using SkiaSharp.Views.Forms;

using System;
using System.Globalization;

using Xamarin.Forms;


namespace MyApp.Helpers
{
    class SKPaintSurfaceEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var skPaintSurfaceEventArgs = value as SKPaintSurfaceEventArgs;
            if (skPaintSurfaceEventArgs == null)
            {
                throw new ArgumentException("Expected value to be of type SKPaintSurfaceEventArgs", nameof(value));
            }
            return skPaintSurfaceEventArgs;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
