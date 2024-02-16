using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using SamplePrism2024.About.Views;
using SamplePrism2024.Shared;

namespace SamplePrism2024.About;

public class AboutModule : IModule
{
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry
            .RegisterForNavigation<AboutView>();
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
        containerProvider
            .Resolve<IRegionManager>()
            .RegisterViewWithRegion(Regions.MainRegion, nameof(AboutView));
    }
}