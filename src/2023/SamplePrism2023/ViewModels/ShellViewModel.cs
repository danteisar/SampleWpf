using System.Windows;
using System.Windows.Input;
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

    public ICommand DragMoveCommand { get; set; }
    public ICommand CloseCommand { get; set; }

    [InjectionMethod]
    public void Initialized()
    {
        EventAggregator
            .GetEvent<CounterChanged>()
            .Subscribe(OnCounterChanged);

        EventAggregator
            .GetEvent<TitleChanged>()
            .Subscribe(x=> Title = x);

        DragMoveCommand = ReactiveCommand.Create(Application.Current.MainWindow!.DragMove);
        CloseCommand = ReactiveCommand.Create(Application.Current.Shutdown);
    }

    private void OnCounterChanged(int value)
    {
        Title = $"Значение счетчика: {value}";
    }
}