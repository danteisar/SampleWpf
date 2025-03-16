using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SamplePrism2025.Shared;
using SamplePrism2025.Shared.Events;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Input;

namespace SamplePrism2025.ViewModels;

/// <summary>
///  Reactive UI sample view model
/// </summary>
internal class ShellViewModel : ReactiveObject
{
    private readonly IRegionManager _regionManager;

    [Reactive] public string Title { get; set; } = "PRISM";
    [Reactive] public bool IsFlipped { get; set; }
    [Reactive] public double Width { get; set; } = 640;

    public ShellViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
    {
        var eventAggregator1 = eventAggregator;
        _regionManager = regionManager;

        eventAggregator1
            .GetEvent<InputChanged>()
            .Subscribe(OnInputChanged);

        this.WhenValueChanged(x => x.IsFlipped)
            .Subscribe(x =>
                eventAggregator1.GetEvent<IsFlippedChanged>()
                    .Publish(x));

        this.WhenPropertyChanged(x => x.IsFlipped)
            .Select(x => x.Value ? 1280d : 640)
            .Subscribe(x => Width = x);

        DragMoveCommand = ReactiveCommand.Create(Application.Current.MainWindow!.DragMove);
        CloseCommand = ReactiveCommand.Create(Application.Current.Shutdown);
        NavigateToAbout = ReactiveCommand.Create(ShowAbout);
        NavigateToSample = ReactiveCommand.Create(ShowSample);
    }

    public ICommand DragMoveCommand { get; set; }
    public ICommand CloseCommand { get; set; }
    public ICommand NavigateToAbout { get; set; }
    public ICommand NavigateToSample { get; set; }

    private void ShowAbout()
    {
        _regionManager.RequestNavigate(Regions.MainRegion, Navigation.AboutPage);
        IsFlipped = false;
    }

    private void ShowSample()
    {
        _regionManager.RequestNavigate(Regions.MainRegion, Navigation.ShowcasePage);
        IsFlipped = false;
    }

    private void OnInputChanged(int? value)
    {
        Title = $"PRISM: {value}";
    }
}