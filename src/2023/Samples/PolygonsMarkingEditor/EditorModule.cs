using PolygonsMarkingEditor.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Samples2023.Shared;

namespace PolygonsMarkingEditor;

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