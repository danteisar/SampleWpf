using SamplePrism2025.About.Views;
using SamplePrism2025.Shared;

namespace SamplePrism2025.About;

public class AboutModule : IModule
{
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry
            .RegisterForNavigation<AboutView>();
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
        //containerProvider
        //    .Resolve<IRegionManager>()
        //    .RegisterViewWithRegion(Regions.MainRegion, nameof(AboutView));
    }
}