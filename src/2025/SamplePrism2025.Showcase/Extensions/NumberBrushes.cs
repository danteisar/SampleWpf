using System.Windows.Media;

namespace SamplePrism2025.Showcase.Extensions;

internal class Extensions
{
    public static readonly Dictionary<NumberValue, Brush> NumberBrushes = new()
    {
        { NumberValue.Error, new SolidColorBrush(Colors.Red) },
        { NumberValue.Less55, new SolidColorBrush(Colors.LightCoral) },
        { NumberValue.Less70, new SolidColorBrush(Colors.LightBlue) },
        { NumberValue.Less86, new SolidColorBrush(Colors.LightGreen) },
        { NumberValue.Perfect, new SolidColorBrush(Colors.DarkOrange) }
    };
}
