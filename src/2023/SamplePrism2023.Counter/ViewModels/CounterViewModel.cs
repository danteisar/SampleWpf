using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MVVM.Commands;
using MVVM.Extensions;
using Prism.Events;
using ReactiveUI;
using SamplePrism2023.Counter.Abstractions;
using SamplePrism2023.Shared.Events;

namespace SamplePrism2023.Counter.ViewModels;

internal class CounterViewModel
{
    private readonly ICounterService _counterService;
    private readonly ICounterAsyncService _counterAsyncService;
    
    public ICounter Model { get; }
    public ICommand IncrementCommand { get; }
    public ICommand DecrementCommand { get; }
    
    /// <summary>
    /// Конструктор для внедрения зависимостей
    /// </summary>
    /// <param name="eventAggregator"></param>
    /// <param name="counterService"></param>
    /// <param name="counterAsyncService"></param>
    /// <param name="model"></param>
    public CounterViewModel(
        IEventAggregator eventAggregator,
        ICounterService counterService, 
        ICounterAsyncService counterAsyncService, 
        ICounter model)
    {
        _counterService = counterService;
        _counterAsyncService = counterAsyncService;
        Model = model;

        // Команда с использованием Reactive UI + Task
        var counterObs = Model.WhenAnyValue(x => x.Value);
        var canIncObs = counterObs.Select(CanIncrement);
        IncrementCommand = ReactiveCommand.CreateFromTask(Increment, canIncObs);

        // Команда на основе библиотеки MVVM
        DecrementCommand = new RelayCommand(Decrement, CanDecrement);
        Model.WhenPropertyChanged(x=>x.Value, DecrementCommand.RaiseCanExecuteChanged);

        // Информирование об изменении счетчика, используя агрегатор событий и возможности Reactive UI
        counterObs.Subscribe(eventAggregator.GetEvent<CounterChanged>().Publish);
    }
    
    private Task Increment() => _counterAsyncService.Increment(Model);
    private bool CanIncrement(int value) => _counterAsyncService.CanIncrement(Model);
    private void Decrement() => _counterService.Decrement(Model);
    private bool CanDecrement() => _counterService.CanDecrement(Model);
}