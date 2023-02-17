using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MVVM.Commands;
using MVVM.Extensions;
using ReactiveUI;
using SampleWpf2023.Abstractions;
using SampleWpf2023.Models;
using SampleWpf2023.Services;
using Splat.ModeDetection;

namespace SampleWpf2023.ViewModels;

internal class CounterViewModel
{
    private readonly ICounterService _counterService;
    private readonly ICounterAsyncService _counterAsyncService;

    public ICounter Model { get; }
    public ICommand IncrementCommand { get; }
    public ICommand DecrementCommand { get; }

    public CounterViewModel()
    {
        _counterAsyncService = new CounterAsyncService();
        _counterService = new CounterService();
        Model = new CounterModel();

        // Reactive UI
        var counterObs = Model.WhenAnyValue(x => x.Value);
        IncrementCommand = ReactiveCommand.CreateFromTask(Increment, counterObs.Select(CanIncrement));
        //DecrementCommand = ReactiveCommand.Create(Decrement, counterObs.Select(CanDecrement));

        // MVVM
        DecrementCommand = new RelayCommand(Decrement, CanDecrement);
        Model.WhenPropertyChanged(x => x.Value, DecrementCommand.RaiseCanExecuteChanged);
    }

    private Task Increment() => _counterAsyncService.Increment(Model);
    private bool CanIncrement(int value) => _counterAsyncService.CanIncrement(Model);
    private void Decrement() => _counterService.Decrement(Model);
    private bool CanDecrement() => _counterService.CanDecrement(Model);
    private bool CanDecrement(int value) => _counterService.CanDecrement(Model);
}