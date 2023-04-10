using System.Collections.ObjectModel;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace PolygonsMarkingEditor.Models;

internal class Polygon : ReactiveObject
{
    public Polygon()
    {
        Points = new();
    }

    [Reactive] public ObservableCollection<Vertex> Points { get; set; }

    [Reactive] public bool IsEnded { get; set; }
}