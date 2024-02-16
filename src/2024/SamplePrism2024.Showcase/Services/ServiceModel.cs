using SamplePrism2024.Showcase.Abstractions;

namespace SamplePrism2024.Showcase.Services;

internal class ServiceModel : IServiceModel
{
    public async Task Push(IModel model)
    {
        if (!model.Input.HasValue) return;
        await Task.Delay(200);
        model.History.Add(model.Input.Value);
    }

    public void Pop(IModel model)
    {
        if (!model.History.Any()) return;
        var input = model.History.Last();
        model.Input = input;
        model.History.Remove(input);
    }

    public void Random(IModel model)
    {
        var rand = new Random();
        int i;
        do
        {
            i = rand.Next(-9, 9);
        } while (i == 0);

        model.Input = i;
    }

    public void Clear(IModel model)
    {
        model.History.Clear();
    }

    public bool Check(IModel model)
    {
        return !model.Input.HasValue && model.History.Any();
    }
}