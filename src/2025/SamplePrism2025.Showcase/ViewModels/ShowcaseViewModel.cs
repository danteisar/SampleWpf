using MVVM.Commands;
using MVVM.Extensions;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SamplePrism2025.Shared;
using SamplePrism2025.Shared.Abstractions;
using SamplePrism2025.Shared.Events;
using System.Collections.Specialized;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Windows.Input;

namespace SamplePrism2025.Showcase.ViewModels;

internal class ShowcaseViewModel : ReactiveObject
{
    private readonly IEventAggregator _eventAggregator;
    private readonly IRegionManager _regionManager;

    public IValueModel Model { get; }
    public IValueService Service { get; }

    [Reactive] public bool CanShowAbout { get; set; } = true;

    public ShowcaseViewModel(IEventAggregator eventAggregator,
                             IRegionManager regionManager,
                             IValueModel model,
                             IValueService service)
    {
        //inject _fields & Props
        _eventAggregator = eventAggregator;
        _regionManager = regionManager;
        Model = model;
        Service = service;

        // MVVM lib observe property for changing
        Model.WhenPropertyChanged(x => x.Value, OnInputChanged);
        Model.ValuesList.CollectionChanged += HistoryOnCollectionChanged;

        // Prism command sample
        About = new DelegateCommand(OpenAbout);
       
        Clear = new RelayCommand(() => Service.Clear(Model), () => Model.ValuesList.Any());
        Pop = new RelayCommand(() => Service.Pop(Model), CanPop);

        // Реактивное программирование
        var inputObs = Model.WhenAnyValue(x => x.Value);
        var boolObs = inputObs.Select(x => x is >= 0 and < 100);
        Push = ReactiveCommand.CreateFromTask(() => Service.Push(Model), boolObs);
        Rand = ReactiveCommand.CreateFromTask(() => Service.Random(Model));
        var ascObs = Observable.Interval(TimeSpan.FromMilliseconds(10))
            .ObserveOn(DispatcherScheduler.Current)
            .Skip(1)
            .Take(99);
        var descObs = ascObs.Select(x => 99 - x);
        ascObs.Concat(descObs)
            .Subscribe(x => Model.Value = (int)x);

        

        // MVVM lib commands samples
        Clear = new RelayCommand(ExecuteClear, Model.ValuesList.Any);
        Pop = new RelayCommand(ExecutePop, CanPop);
    }

    public ICommand Clear { get; set; }
    public ICommand Push { get; set; }
    public ICommand Pop { get; set; }
    public ICommand Rand { get; set; }
    public ICommand About { get; set; }

    private void ExecutePop() => Service.Pop(Model);
    private bool CanPop() => Service.Check(Model);
    private void ExecuteClear() => Service.Clear(Model);
    private void OpenAbout() => _regionManager.RequestNavigate(Regions.MainRegion, Navigation.AboutPage);

    /// <summary>
    /// Check commands
    /// </summary>
    private void CheckCommands(object sender, NotifyCollectionChangedEventArgs e)
    {
        Clear.RaiseCanExecuteChanged();
        Pop.RaiseCanExecuteChanged();
    }

    private void HistoryOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        Clear.RaiseCanExecuteChanged();
    }

    /// <summary>
    /// Event aggregator && check pop command
    /// </summary>
    private void OnInputChanged()
    {
        Pop.RaiseCanExecuteChanged();

        _eventAggregator
            .GetEvent<InputChanged>()
            .Publish(Model.Value);
    }
}
