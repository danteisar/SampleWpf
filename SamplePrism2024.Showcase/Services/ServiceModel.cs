using SamplePrism2024.Showcase.Abstractions;

namespace SamplePrism2024.Showcase.Services;

internal class ServiceModel : IServiceModel
{
    public async Task Push(IModel model)
    {
        if (model.Input == 0) return;
        await Task.Delay(200);
        model.History.Add(model.Input);
        model.Input = 0;
    }

    public void Pop(IModel model)
    {
        if (!model.History.Any()) return;
        model.Input = model.History.Last();
        model.History.Remove(model.Input);
    }

    public void Random(IModel model)
    {
        var rand = new Random();
        model.Input = rand.Next(-9,9);
        model.History.Remove(model.Input);
    }

    public void Clear(IModel model)
    {
        model.History.Clear();
    }
}