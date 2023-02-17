using System.Collections.Generic;
using System.Windows.Media;
using SamplePrism2023.Counter.Components;

namespace SamplePrism2023.Counter.Resources;

internal class Extensions
{
    public static readonly Dictionary<NumberValue, Brush> NumberBrushes = new()
    {
        { NumberValue.LessZero, new SolidColorBrush(Colors.LightCoral) },
        { NumberValue.Zero, new SolidColorBrush(Colors.LightBlue) },
        { NumberValue.UpperZero, new SolidColorBrush(Colors.LightGreen) },
    };
}
