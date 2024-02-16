using System.Windows;
using System.Windows.Input;
using Prism.Events;
using ReactiveUI.Fody.Helpers;
using ReactiveUI;
using SamplePrism2024.Shared.Events;
using Unity;

namespace SamplePrism2024.ViewModels;

internal class ShellViewModel : ReactiveObject
{
    [Reactive] public string Title { get; set; } = "PRISM";

    [Dependency]
    public IEventAggregator EventAggregator { get; init; }

    public ICommand DragMoveCommand { get; set; }
    public ICommand CloseCommand { get; set; }

    [InjectionMethod]
    public void Initialized()
    {
        EventAggregator
            .GetEvent<InputChanged>()
            .Subscribe(OnInputChanged);

        DragMoveCommand = ReactiveCommand.Create(Application.Current.MainWindow!.DragMove);
        CloseCommand = ReactiveCommand.Create(Application.Current.Shutdown);
    }

    private void OnInputChanged(int value)
    {
        Title = $"PRISM: {value}";
    }
}