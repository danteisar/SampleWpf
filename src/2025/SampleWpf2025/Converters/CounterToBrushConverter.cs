﻿using System.Globalization;
using MVVM.Converters;
using SampleWpf2025.Extensions;
using static SampleWpf2025.Extensions.Extensions;

namespace SampleWpf2025.Converters;

internal class CounterToBrushConverter : ConverterBase<CounterToBrushConverter>
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not int counter) return NumberBrushes[NumberValue.Error];

        if (value is < 0 or > 99) return NumberBrushes[NumberValue.Error];

        return counter < 55
            ? NumberBrushes[NumberValue.Less55]
            : counter < 70
                ? NumberBrushes[NumberValue.Less70]
                : counter < 86
                    ? NumberBrushes[NumberValue.Less86]
                    : NumberBrushes[NumberValue.Perfect];
    }
}