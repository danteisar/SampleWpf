using System;
using System.Threading.Tasks;
using SamplePrism2023.Counter.Abstractions;

namespace SamplePrism2023.Counter.Services;

internal class CounterAsyncService : ICounterAsyncService
{
    private readonly Random _random = new();    
    private const int Limit = 100;
    private Task Oops() => Task.Delay(10);

    public async Task Increment(ICounter counter)
    {
        if (!CanIncrement(counter)) return;
        
        await Oops();

        var inc = _random.Next(1, 10);
        if (inc + counter.Value > Limit)
            counter.Value = Limit;
        else
            counter.Value += inc;
    }

    public async Task Decrement(ICounter counter)
    {
        if (!CanDecrement(counter)) return;
        
        await Oops();

        var dec = _random.Next(1, 10);
        if (dec - counter.Value < -Limit)
            counter.Value = -Limit;
        else
            counter.Value -= dec;
    }

    public bool CanIncrement(ICounter counter)
        => counter.Value < Limit;

    public bool CanDecrement(ICounter counter)
        => counter.Value > -Limit;
}