using System;
using System.Globalization;
using Xamarin.Forms;

namespace Movies.Converters
{
    public class PosterPathConverter : IValueConverter 
    {
        public ImageSource EmptyImageSource { get; set; }
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string path))
            {
                return EmptyImageSource;
            }

            
            return string.IsNullOrWhiteSpace(path) ? EmptyImageSource : $"https://image.tmdb.org/t/p/w185{path}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}