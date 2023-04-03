using PolygonsMarking.Editor.Views;
using PolygonsMarking.Shared;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace PolygonsMarking.Editor;

public class EditorModule : IModule
{

    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry
            .RegisterForNavigation<MarkupEditorView>();
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
        containerProvider
            .Resolve<IRegionManager>()
            .RegisterViewWithRegion(Regions.ContentRegion, nameof(MarkupEditorView));
    }
}   