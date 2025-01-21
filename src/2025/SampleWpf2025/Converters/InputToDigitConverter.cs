using MVVM.Converters;
using System.Globalization;

namespace SampleWpf2025.Converters;

internal class InputToDigitConverter : ConverterBase<InputToDigitConverter>
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not int counter) return 0;

        if (!int.TryParse(parameter.ToString(), out var index)) return 0;

        if (index == 0 && counter > 0 && counter < 10) return counter;

        if (index == 0 && counter < 100) return counter % 10;

        if (index == 1 && counter < 10) return 0;

        if (index == 1 && counter < 100) return counter / 10;

        return 0;
    }
}
