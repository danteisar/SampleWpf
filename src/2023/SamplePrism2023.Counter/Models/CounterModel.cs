using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SamplePrism2023.Counter.Abstractions;

namespace SamplePrism2023.Counter.Models;

internal class CounterModel : ReactiveObject, ICounter
{
    [Reactive] public int Value { get; set; }
}