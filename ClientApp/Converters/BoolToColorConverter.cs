using System;
using System.Globalization;
using System.Windows.Media;

namespace ClientApp.Converters
{
    public class BoolToColorConverter : Extensions.ConvertorBase<BoolToColorConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool) value == true)
                return Brushes.Green;
            else
                return Brushes.Red;
        }
    }
}