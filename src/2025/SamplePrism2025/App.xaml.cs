using SamplePrism2025.ViewModels;
using SamplePrism2025.About;
using SamplePrism2025.Showcase;
using SamplePrism2025.Views;
using System.Windows;

namespace SamplePrism2025;

public partial class App
{
    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry
            .RegisterSingleton<ShellViewModel>();
    }

    protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
    {
        moduleCatalog
            .AddModule<AboutModule>()
            .AddModule<ShowcaseModule>();
    }

    protected override Window CreateShell() => Container.Resolve<ShellView>();
}
