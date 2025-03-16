using System.Windows.Input;
using SamplePrism2025.Shared;

namespace SamplePrism2025.About.ViewModels;

internal class AboutViewModel : BindableBase
{
    private readonly IRegionManager _regionManager;

    public AboutViewModel(IRegionManager regionManager)
    {
        _regionManager = regionManager;

        BackCommand = new DelegateCommand(Back);
    }

    private void Back()
    {
        _regionManager
            .RequestNavigate(Regions.MainRegion, Navigation.ShowcasePage);
    }

    public ICommand BackCommand { get; }
}
