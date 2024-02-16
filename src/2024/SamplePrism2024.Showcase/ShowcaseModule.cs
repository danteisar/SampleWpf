using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using SamplePrism2024.Shared;
using SamplePrism2024.Showcase.Abstractions;
using SamplePrism2024.Showcase.Models;
using SamplePrism2024.Showcase.Services;
using SamplePrism2024.Showcase.ViewModels;
using SamplePrism2024.Showcase.Views;

namespace SamplePrism2024.Showcase;

public class ShowcaseModule : IModule
{
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry
            .Register<IModel, Model>()
            .RegisterSingleton<IServiceModel, ServiceModel>();

        //containerRegistry.Register<ShowcaseViewModel>();
        containerRegistry.RegisterScoped<ShowcaseViewModel>();
        //containerRegistry.RegisterSingleton<ShowcaseViewModel>();

        containerRegistry
            .RegisterForNavigation<ShowcaseView>();

        containerRegistry
            .RegisterForNavigation<CompositeView>();
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
        var rm = containerProvider
            .Resolve<IRegionManager>();

        rm.RegisterViewWithRegion(Regions.MainRegion, nameof(ShowcaseView));
        rm.RegisterViewWithRegion(Regions.SecondRegion, nameof(CompositeView));
    }
}