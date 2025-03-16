namespace SamplePrism2025.Showcase.ViewModels;

internal class CompositeViewModel
{
    public ShowcaseViewModel A { get; set; }
    public ShowcaseViewModel B { get; set; }

    /// <summary>
    /// Using scope sample
    /// </summary>
    public CompositeViewModel(IContainerProvider provider)
    {
        using var scope = provider.CreateScope();
        A = scope.Resolve<ShowcaseViewModel>();
        B = scope.Resolve<ShowcaseViewModel>();
    }
}