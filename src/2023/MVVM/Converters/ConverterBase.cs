using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace MVVM.Converters;

public abstract class ConverterBase<T> : MarkupExtension, IValueConverter
    where T : class, new()
{
    public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

    public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => new NotImplementedException();

    #region MarkupExtension members

    public  sealed override object ProvideValue(IServiceProvider serviceProvider) => Converter;

    private static readonly T Converter = new();

    #endregion
}   