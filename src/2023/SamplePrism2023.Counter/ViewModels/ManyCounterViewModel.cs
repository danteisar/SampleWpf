using System.Windows.Input;
using Prism.Commands;
using Prism.Regions;
using ReactiveUI;
using SamplePrism2023.Shared;

namespace SamplePrism2023.Counter.ViewModels;

internal class ManyCounterViewModel : ReactiveObject
{
    private readonly IRegionManager _regionManager;
    public ManyCounterViewModel(
        CounterViewModel counter1,
        CounterViewModel counter2,
        IRegionManager regionManager)
    {
        Counter1 = counter1;
        Counter2 = counter2;
        _regionManager = regionManager;
        AboutCommand = new DelegateCommand(About);
    }

    public ICommand AboutCommand { get; }
    public CounterViewModel Counter1 { get; }
    public CounterViewModel Counter2 { get; }

    private void About()
    {
        _regionManager
            .RequestNavigate(Regions.MainRegion, Navigation.AboutPage);
    }
}