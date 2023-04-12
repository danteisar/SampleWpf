using PolygonsMarkingEditor.Tools;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace PolygonsMarkingEditor.Models;

internal class Vertex : ReactiveObject
{
    [Reactive] public double X { get; set; }
    [Reactive] public double Y { get; set; }
    [Reactive] public bool IsSelected { get; set; }

    public Vertex()
    {
        X = 50;
        Y = 50;
    }

    public Vertex(Vertex s)
    {
        X = s.X - Extensions.ElementSize;
        Y = s.Y - Extensions.ElementSize;
    }
}