using System.Windows;
using System.Windows.Input;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace PolygonsMarking.ViewModels;

internal class ShellViewModel: ReactiveObject
{
    [Reactive] public string Title { get; set; }

    public ShellViewModel()
    {
        Title = "POLY MARKER EDITOR";

        DragMoveCommand = ReactiveCommand.Create(Application.Current.MainWindow!.DragMove);
        CloseCommand = ReactiveCommand.Create(Application.Current.Shutdown);
    }

    public ICommand DragMoveCommand { get; set; }
    public ICommand CloseCommand { get; set; }
}