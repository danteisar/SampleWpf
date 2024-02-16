using System.Windows.Media;

namespace SamplePrism2024.Showcase.Converters;

internal class Extensions
{
    public static readonly Dictionary<NumberValue, Brush> NumberBrushes = new()
    {
        { NumberValue.LessZero, new SolidColorBrush(Colors.LightBlue) },
        { NumberValue.Zero, new SolidColorBrush(Colors.LightCoral) },
        { NumberValue.UpperZero, new SolidColorBrush(Colors.LightGreen) }
    };
}
