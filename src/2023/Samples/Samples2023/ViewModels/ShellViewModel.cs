using System.Windows;
using System.Windows.Input;
using Prism.Regions;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Samples2023.Shared;

namespace Samples2023.ViewModels;

internal class ShellViewModel: ReactiveObject
{
    [Reactive] public string Title { get; set; }

    public ShellViewModel(IRegionManager regionManager)
    {
        var regionManager1 = regionManager;
        Title = "WPF 2023 SAMPLES";

        DragMoveCommand = ReactiveCommand.Create(Application.Current.MainWindow!.DragMove);
        CloseCommand = ReactiveCommand.Create(Application.Current.Shutdown);

        NavigateA = ReactiveCommand.Create(()=> regionManager1.RequestNavigate(Regions.ContentRegion, Navigation.AboutPage));
        Navigate1 = ReactiveCommand.Create(()=> regionManager1.RequestNavigate(Regions.ContentRegion, Navigation.EditorPage));
    }

    public ICommand DragMoveCommand { get; set; }
    public ICommand CloseCommand { get; set; }
    public ICommand Navigate1 { get; set; }
    public ICommand NavigateA { get; set; }

}