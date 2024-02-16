using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SamplePrism2024.Shared;

namespace SamplePrism2024.About.ViewModels;

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
