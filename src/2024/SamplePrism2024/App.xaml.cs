using System.Windows;
using Prism.Ioc;
using Prism.Modularity;
using SamplePrism2024.About;
using SamplePrism2024.Showcase;
using SamplePrism2024.ViewModels;
using SamplePrism2024.Views;

namespace SamplePrism2024;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
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