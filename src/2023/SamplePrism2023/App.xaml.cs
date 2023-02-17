using System.Windows;
using Prism.Ioc;
using Prism.Modularity;
using SamplePrism2023.Counter;
using SamplePrism2023.Showcase;
using SamplePrism2023.Views;

namespace SamplePrism2023;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
    {
        moduleCatalog
            .AddModule<CounterModule>()
            .AddModule<ShowcaseModule>();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        
    }

    protected override Window CreateShell() 
        => Container.Resolve<ShellView>();
}