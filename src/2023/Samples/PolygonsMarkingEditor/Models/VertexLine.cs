using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace PolygonsMarkingEditor.Models
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
