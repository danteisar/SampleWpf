using System.Windows;
using PolygonsMarking.Editor;
using PolygonsMarking.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace PolygonsMarking;

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