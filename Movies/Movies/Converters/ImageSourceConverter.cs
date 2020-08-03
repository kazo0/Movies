using System;
using System.Globalization;
using Xamarin.Forms;

namespace Movies.Converters
{
    public class ImageSourceConverter : IValueConverter 
    {
        public ImageSource EmptyImageSource { get; set; }
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string url))
            {
                return EmptyImageSource;
            }

            return string.IsNullOrWhiteSpace(url) ? EmptyImageSource : url;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}