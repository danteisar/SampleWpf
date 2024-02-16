using System.Collections.ObjectModel;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SamplePrism2024.Showcase.Abstractions;

namespace SamplePrism2024.Showcase.Models;

public class Model : ReactiveObject, IModel
{
    [Reactive] public int Input { get; set; }

    public ObservableCollection<int> History { get; } = new();
}