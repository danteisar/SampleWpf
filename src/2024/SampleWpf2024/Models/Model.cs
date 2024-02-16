using System.Collections.ObjectModel;
using MVVM;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SampleWpf2024.Abstractions;

namespace SampleWpf2024.Models;

public class Model : ReactiveObject, IModel
{
    [Reactive] public int Input { get; set; }

    public ObservableCollection<int> History { get; } = new();
}