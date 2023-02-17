using System.Collections.Generic;
using System.Windows.Media;

namespace SampleWpf2023.Resources;

internal class Extensions
{
    public static readonly Dictionary<NumberValue, Brush> NumberBrushes = new()
    {
        { NumberValue.LessZero, new SolidColorBrush(Colors.LightCoral) },
        { NumberValue.Zero, new SolidColorBrush(Colors.LightBlue) },
        { NumberValue.UpperZero, new SolidColorBrush(Colors.LightGreen) },
    };
}
