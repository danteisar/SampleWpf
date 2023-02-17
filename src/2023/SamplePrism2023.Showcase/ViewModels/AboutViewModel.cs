using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SamplePrism2023.Shared;

namespace SamplePrism2023.Showcase.ViewModels;

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
            .RequestNavigate(Regions.MainRegion, Navigation.MainPage);
    }

    public ICommand BackCommand { get; }
}