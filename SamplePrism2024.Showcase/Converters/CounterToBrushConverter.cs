using System.Globalization;
using MVVM.Converters;
using static SamplePrism2024.Showcase.Converters.Extensions;

namespace SamplePrism2024.Showcase.Converters;

internal class CounterToBrushConverter : ConverterBase<CounterToBrushConverter>
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not int counter) return NumberBrushes[NumberValue.Zero];

        return counter < 0
            ? NumberBrushes[NumberValue.LessZero]
            : counter == 0
                ? NumberBrushes[NumberValue.Zero]
                : NumberBrushes[NumberValue.UpperZero];
    }
}