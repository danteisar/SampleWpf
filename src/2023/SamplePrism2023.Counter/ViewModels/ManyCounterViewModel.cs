﻿using System.Windows.Input;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using ReactiveUI;
using SamplePrism2023.Shared;
using SamplePrism2023.Shared.Events;

namespace SamplePrism2023.Counter.ViewModels;

internal class ManyCounterViewModel : ReactiveObject
{
    private readonly IEventAggregator _eventAggregator;
    private readonly IRegionManager _regionManager;
    public ManyCounterViewModel(
        IEventAggregator eventAggregator,
        CounterViewModel counter1,
        CounterViewModel counter2,
        IRegionManager regionManager)
    {
        Counter1 = counter1;
        Counter2 = counter2;
        _eventAggregator = eventAggregator;
        _regionManager = regionManager;
        AboutCommand = new DelegateCommand(About);

        _eventAggregator.GetEvent<TitleChanged>().Publish("PRISM WPF 2023");
    }

    public ICommand AboutCommand { get; }
    public CounterViewModel Counter1 { get; }
    public CounterViewModel Counter2 { get; }

    private void About()
    {
        _regionManager.RequestNavigate(Regions.MainRegion, Navigation.AboutPage);
        _eventAggregator.GetEvent<TitleChanged>().Publish("PRISM WPF 2023");
    }
}