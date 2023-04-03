using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace PolygonsMarking.Editor.Models;

internal class Vertex : ReactiveObject
{
    [Reactive] public double X { get; set; }
    [Reactive] public double Y { get; set; }

    public Vertex()
    {
        X = 50;
        Y = 50;
    }

    public Vertex(Vertex s)
    {
        X = s.X;
        Y = s.Y;
    }
}