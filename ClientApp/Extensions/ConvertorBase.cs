using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Extensions
{
    public abstract class ConvertorBase<T> : MarkupExtension, IValueConverter
        where T : ConvertorBase<T>, new()
    {
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => new NotImplementedException();

        #region MarkupExtension members

        public override object ProvideValue(IServiceProvider serviceProvider) => _converter ??= new T();

        private static T _converter;

        #endregion MarkupExtension members
    }
}