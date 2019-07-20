using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PingAlerter.Common.Converters
{
    public class IsEqualOrGreaterThanConverter : IValueConverter
    {
        public static readonly IValueConverter Instance = new IsEqualOrGreaterThanConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int intValue = System.Convert.ToInt32(value);
            int compareToValue = System.Convert.ToInt32(parameter);

            return intValue >= compareToValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
