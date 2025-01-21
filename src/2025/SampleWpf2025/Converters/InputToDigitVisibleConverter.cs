using MVVM.Converters;
using System.Globalization;
using System.Windows;

namespace SampleWpf2025.Converters;

internal class InputToDigitVisibleConverter : ConverterBase<InputToDigitVisibleConverter>
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not int counter) return Visibility.Collapsed;

        if (!int.TryParse(parameter.ToString(), out var index)) return Visibility.Collapsed;

        switch (index)
        {
            case 0:
                if (counter == 0 ||
                    counter == 2 ||
                    counter == 3 ||
                    counter == 5 ||
                    counter == 6 ||
                    counter == 7 ||
                    counter == 8 ||
                    counter == 9) return Visibility.Visible;
                break;
            case 1:
                if (counter == 0 ||
                    counter == 4 ||
                    counter == 5 ||
                    counter == 6 ||
                    counter == 8 ||
                    counter == 9) return Visibility.Visible;
                break;
            case 2:
                if (counter == 0 ||
                    counter == 1 ||
                    counter == 2 ||
                    counter == 3 ||
                    counter == 4 ||
                    counter == 7 ||
                    counter == 8 ||
                    counter == 9) return Visibility.Visible;
                break;
            case 3:
                if (counter == 2 ||
                    counter == 3 ||
                    counter == 4 ||
                    counter == 5 ||
                    counter == 6 ||
                    counter == 8 ||
                    counter == 9) return Visibility.Visible;
                break;
            case 4:
                if (counter == 0 ||
                    counter == 2 ||
                    counter == 6 ||
                    counter == 8) return Visibility.Visible;
                break;
            case 5:
                if (counter == 0 ||
                    counter == 1 ||
                    counter == 3 ||
                    counter == 4 ||
                    counter == 5 ||
                    counter == 6 ||
                    counter == 7 ||
                    counter == 8 ||
                    counter == 9) return Visibility.Visible;
                break;
            case 6:
                if (counter == 0 ||
                    counter == 2 ||
                    counter == 3 ||
                    counter == 5 ||
                    counter == 6 ||
                    counter == 8 ||
                    counter == 9) return Visibility.Visible;
                break;
        }
        return Visibility.Collapsed;
    }
}
