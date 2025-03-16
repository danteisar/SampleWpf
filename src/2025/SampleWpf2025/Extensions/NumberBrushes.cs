using System.Windows.Media;

namespace SampleWpf2025.Extensions;

internal class Extensions
{
    private static readonly GradientStopCollection Less55 = new([new GradientStop(Colors.Red, 0.0), new GradientStop(Colors.LightCoral, 0.8), new GradientStop(Colors.Red, 1.0)]);
    private static readonly GradientStopCollection Less70 = new ([new GradientStop(Colors.Blue, 0.0), new GradientStop(Colors.LightBlue, 0.8), new GradientStop(Colors.Blue, 0.0)]);
    private static readonly GradientStopCollection Less86 = new ([new GradientStop(Colors.Green, 0.0), new GradientStop(Colors.LightGreen, 0.8), new GradientStop(Colors.Green, 0.0)]);
    private static readonly GradientStopCollection Perfect = new ([new GradientStop(Colors.Gold, 0.0), new GradientStop(Colors.LightGreen, 0.8), new GradientStop(Colors.Gold, 0.0)]);
    
    //public static readonly Dictionary<NumberValue, Brush> NumberBrushes = new()
    //{
    //    { NumberValue.Less55, new RadialGradientBrush(Less55) },
    //    { NumberValue.Less70, new RadialGradientBrush(Less70) },
    //    { NumberValue.Less86, new RadialGradientBrush(Less86) },
    //    { NumberValue.Perfect, new RadialGradientBrush(Perfect) }
    //};

    public static readonly Dictionary<NumberValue, Brush> NumberBrushes = new()
    {
        { NumberValue.Error, new SolidColorBrush(Colors.Red) },
        { NumberValue.Less55, new SolidColorBrush(Colors.LightCoral) },
        { NumberValue.Less70, new SolidColorBrush(Colors.LightBlue) },
        { NumberValue.Less86, new SolidColorBrush(Colors.LightGreen) },
        { NumberValue.Perfect, new SolidColorBrush(Colors.DarkOrange) }
    };
}
