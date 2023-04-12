using System.Windows.Media;
using MaterialDesignThemes.Wpf;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace PolygonsMarkingEditor.Models;

internal class Image : ReactiveObject
{
    [Reactive] public Brush Brush { get; set; } = new SolidColorBrush(new PaletteHelper().GetTheme().Background);
    [Reactive] public double Width { get; set; } = 1260;
    [Reactive] public double Height { get; set; } = 660;
}