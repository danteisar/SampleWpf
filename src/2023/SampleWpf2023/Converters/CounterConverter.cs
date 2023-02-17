using System;
using System.Globalization;
using MVVM.Converters;

namespace SampleWpf2023.Converters;

internal class CounterConverter : ConverterBase<CounterConverter>
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is int c 
            ? $"Значение счетчика: {c}" 
            : "MVVM";
    }
}