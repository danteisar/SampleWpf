using SampleWpf2025.Abstractions;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace SampleWpf2025.Services
{
    internal class ServiceModel
    {
        public async Task Push(IModel model)
        {
            if (model.Input < 0 || model.Input > 99) return;
            await Task.Delay(100);
            model.History.Add(model.Input);
            model.Input = 0;
        }

        public void Pop(IModel model)
        {
            if (!model.History.Any()) return;
            model.Input = model.History.First();
            model.History.RemoveAt(0);
        }

        public async Task Random(IModel model)
        {
            var randObs = Observable.Interval(TimeSpan.FromMilliseconds(10))
                     .ObserveOn(DispatcherScheduler.Current)
                     .Take(20)
                     .Select(x=>System.Random.Shared.Next(0, 90))
                     .Subscribe(x=>model.Input = x);
            for (var i = 0; i < 3; i++)
            {
                await Task.Delay(500);
                model.Input += System.Random.Shared.Next(0, 3);
            }
        }

        public void Clear(IModel model)
        {
            model.History.Clear();
        }
    }
}
