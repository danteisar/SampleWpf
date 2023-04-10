using System.Windows;
using PolygonsMarkingEditor;
using Prism.Ioc;
using Prism.Modularity;
using Samples2023.Views;

namespace Samples2023;

public partial class App
{
    protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
    {
        moduleCatalog.AddModule<EditorModule>();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
       containerRegistry.RegisterForNavigation<AboutView>();

       //Container.Resolve<IRegionManager>().RegisterViewWithRegion(Regions.ContentRegion, nameof(AboutView));
    }

    protected override Window CreateShell() => Container.Resolve<ShellView>();
}