using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace PolygonsMarking.Editor.Models
{
    internal class VertexLine : ReactiveObject
    {
        public VertexLine(Vertex start, Vertex end)
        {
            Start = start;
            End = end;
        }

        [Reactive] public Vertex Start { get; set; }
        [Reactive] public Vertex End { get; set; }
    }
}
