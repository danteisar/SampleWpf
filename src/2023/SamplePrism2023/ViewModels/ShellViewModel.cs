using Prism.Events;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SamplePrism2023.Shared.Events;
using Unity;

namespace SamplePrism2023.ViewModels;

internal class ShellViewModel: ReactiveObject
{
    [Reactive]
    public string Title { get; set; }

    [Dependency]
    public IEventAggregator EventAggregator { get; init; }

    [InjectionMethod]
    public void Initialized()
    {
        EventAggregator
            .GetEvent<CounterChanged>()
            .Subscribe(OnCounterChanged);
    }

    private void OnCounterChanged(int value)
    {
        Title = $"Значение счетчика: {value}";
    }
}