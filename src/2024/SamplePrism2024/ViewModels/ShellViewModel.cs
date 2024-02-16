using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Input;
using DynamicData.Binding;
using Prism.Events;
using Prism.Regions;
using ReactiveUI.Fody.Helpers;
using ReactiveUI;
using SamplePrism2024.Shared;
using SamplePrism2024.Shared.Events;
using SamplePrism2024.Shared.Extensions;

namespace SamplePrism2024.ViewModels;

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
            .Select(x=>x.Value ? 1280d : 640)
            .Subscribe(x=>Width = x);

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