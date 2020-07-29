using System;
using System.Globalization;
using Humanizer;
using Xamarin.Forms;

namespace XFBlogClient.ValueConverters
{
    public class DateTimeHumanizerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is null ? "" : ((DateTime) value).ToUniversalTime().Humanize();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}