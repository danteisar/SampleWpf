using MVVM.Commands;
using MVVM.Extensions;
using ReactiveUI;
using SampleWpf2025.Abstractions;
using SampleWpf2025.Models;
using SampleWpf2025.Services;
using System.Collections.Specialized;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Windows.Input;

namespace SampleWpf2025.ViewModels;

internal class MainViewModel
{
    public IModel Model { get; } = new MainModel();
    public ServiceModel Service { get; } = new();

    public MainViewModel()
    {
        Model.WhenPropertyChanged(x => x.Input, CheckCommands);
        Model.History.CollectionChanged += HistoryOnCollectionChanged;
        Clear = new RelayCommand(() => Service.Clear(Model), () => Model.History.Any());
        Pop = new RelayCommand(() => Service.Pop(Model), CanPop);
        //Push = new RelayCommand(() => Service.Push(Model), CanPush);
        //Rand = new RelayCommand(() => Service.Random(Model));

        // Реактивное программирование
        var inputObs = Model.WhenAnyValue(x => x.Input);
        var boolObs = inputObs.Select(x => x is >= 0 and < 100);
        Push = ReactiveCommand.CreateFromTask(() => Service.Push(Model), boolObs);
        Rand = ReactiveCommand.CreateFromTask(() => Service.Random(Model));
        var ascObs = Observable.Interval(TimeSpan.FromMilliseconds(10))
            .ObserveOn(DispatcherScheduler.Current)
            .Skip(1)
            .Take(99);
        var descObs = ascObs.Select(x => 99 - x);
        Observable.Concat(ascObs, descObs)
            .Subscribe(x => Model.Input = (int)x);
    }

    public ICommand Clear { get; set; }
    public ICommand Push { get; set; }
    public ICommand Pop { get; set; }
    public ICommand Rand { get; set; }

    private bool CanPop() => Model.History.Any();
    private bool CanPush() => Model.Input >= 0 && Model.Input <= 99;

    private void HistoryOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        Clear.RaiseCanExecuteChanged();
    }
    private void CheckCommands()
    {
        Pop.RaiseCanExecuteChanged();
        //Push.RaiseCanExecuteChanged();
    }
}
