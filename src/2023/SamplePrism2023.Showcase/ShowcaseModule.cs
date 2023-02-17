using Prism.Ioc;
using Prism.Modularity;
using SamplePrism2023.Showcase.Views;

namespace SamplePrism2023.Showcase;

public class ShowcaseModule : IModule
{
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry
            .RegisterForNavigation<AboutView>();
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
       
    }
}