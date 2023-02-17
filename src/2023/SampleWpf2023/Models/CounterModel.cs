using MVVM;
using SampleWpf2023.Abstractions;

namespace SampleWpf2023.Models;

internal class CounterModel : BindableBase, ICounter
{
    private int _counter;
    public int Value
    {
        get => _counter;
        set => SetProperty(ref _counter, value);
    }
}