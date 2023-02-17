using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using SamplePrism2023.Counter.Abstractions;
using SamplePrism2023.Counter.Models;
using SamplePrism2023.Counter.Services;
using SamplePrism2023.Counter.Views;
using SamplePrism2023.Shared;

namespace SamplePrism2023.Counter;

public class CounterModule : IModule
{
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry
            .Register<ICounter, CounterModel>()
            .RegisterSingleton<ICounterService, CounterService>()
            .RegisterSingleton<ICounterAsyncService, CounterAsyncService>()
            ;

        containerRegistry
            .RegisterForNavigation<ManyCounterView>();
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
        containerProvider
            .Resolve<IRegionManager>()
            .RegisterViewWithRegion(Regions.MainRegion, nameof(ManyCounterView));
    }
}