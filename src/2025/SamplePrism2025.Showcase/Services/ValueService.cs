using SamplePrism2025.Shared.Abstractions;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace SamplePrism2025.Showcase.Services;

internal class ValueService : IValueService
{
    public async Task Push(IValueModel model)
    {        
        if (model.Value < 0 || model.Value > 99) return;
        await Task.Delay(1000);
        model.ValuesList.Add(model.Value);
        model.Value = 0;
    }

    public void Pop(IValueModel model)
    {
        if (!model.ValuesList.Any()) return;
        model.Value = model.ValuesList.First();
        model.ValuesList.RemoveAt(0);
    }

    public async Task Random(IValueModel model)
    {
        var randObs = Observable.Interval(TimeSpan.FromMilliseconds(10))
                 .ObserveOn(DispatcherScheduler.Current)
                 .Take(20)
                 .Select(x => System.Random.Shared.Next(0, 90))
                 .Subscribe(x => model.Value = x);
        for (var i = 0; i < 3; i++)
        {
            await Task.Delay(500);
            model.Value += System.Random.Shared.Next(0, 3);
        }
    }

    public void Clear(IValueModel model)
    {
        model.ValuesList.Clear();
    }

    public bool Check(IValueModel model)
    {
        return model.Value == 0 && model.ValuesList.Any();
    }
}
