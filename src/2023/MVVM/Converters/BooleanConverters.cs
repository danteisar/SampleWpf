using System;
using System.Collections.Generic;
using System.Globalization;

namespace MVVM.Converters;

public class BooleanConverters<T, TMarkup> : ConverterBase<TMarkup>
    where TMarkup : BooleanConverters<T, TMarkup>, new()
{
    public BooleanConverters(T trueValue, T falseValue)
    {
        TrueValue = trueValue;
        FalseValue = falseValue;
    }

    public T TrueValue { get; set; }

    public T FalseValue { get; set; }

    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        => value is true ? TrueValue : FalseValue;

    public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => value is T tValue && EqualityComparer<T>.Default.Equals(tValue, TrueValue);
}