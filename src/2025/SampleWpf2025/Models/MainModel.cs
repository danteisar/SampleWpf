using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SampleWpf2025.Abstractions;
using System.Collections.ObjectModel;

namespace SampleWpf2025.Models
{
    internal class MainModel : ReactiveObject, IModel
    {
        [Reactive] public int Input { get; set; }

        public ObservableCollection<int> History { get; } = new();
    }
}
