using Prism.Ioc;
using Prism.Modularity;
using SamplePrism2024.Showcase.Abstractions;
using SamplePrism2024.Showcase.Models;
using SamplePrism2024.Showcase.Services;
using SamplePrism2024.Showcase.Views;

namespace SamplePrism2024.Showcase;

public class ShowcaseModule : IModule
{
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry
            .Register<IModel, Model>()
            .RegisterSingleton<IServiceModel, ServiceModel>();

        containerRegistry
            .RegisterForNavigation<ShowcaseView>();
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
       
    }
}