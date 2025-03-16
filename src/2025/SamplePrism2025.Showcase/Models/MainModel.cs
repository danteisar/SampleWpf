using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SamplePrism2025.Shared.Abstractions;
using System.Collections.ObjectModel;

namespace SamplePrism2025.Showcase.Models;

internal class MainModel : ReactiveObject, IValueModel
{
    [Reactive] public int Value { get; set; }
    public ObservableCollection<int> ValuesList { get; set; } = [44, 55, 70, 86];
}
