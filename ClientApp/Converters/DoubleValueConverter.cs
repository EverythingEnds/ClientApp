using Extensions;
using System;
using System.Globalization;

namespace ClientApp.Converters
{
    internal class DoubleValueConverter : ConvertorBase<DoubleValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((double) value).ToString("0.##", culture) + " усл.ед.";
        }
    }
}