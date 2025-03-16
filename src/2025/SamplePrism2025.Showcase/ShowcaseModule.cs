using SamplePrism2025.Shared;
using SamplePrism2025.Shared.Abstractions;
using SamplePrism2025.Showcase.Models;
using SamplePrism2025.Showcase.Services;
using SamplePrism2025.Showcase.ViewModels;
using SamplePrism2025.Showcase.Views;

namespace SamplePrism2025.Showcase;

public class ShowcaseModule : IModule
{
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry
            .Register<IValueModel, MainModel>()
            .RegisterSingleton<IValueService, ValueService>();

        //containerRegistry.Register<ShowcaseViewModel>();
        //containerRegistry.RegisterScoped<ShowcaseViewModel>();
        containerRegistry.RegisterSingleton<ShowcaseViewModel>();

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