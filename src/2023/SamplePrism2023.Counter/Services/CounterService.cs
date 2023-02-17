using System;
using System.Threading;
using SamplePrism2023.Counter.Abstractions;

namespace SamplePrism2023.Counter.Services;

internal class CounterService : ICounterService
{

    private readonly Random _random = new();
    private const int Limit = 100; 
    
    private void Oops() => Thread.Sleep(20);

    public void Increment(ICounter counter)
    {
        if (!CanIncrement(counter)) return;
        Oops();

        var inc = _random.Next(1, 10);
        if (inc + counter.Value > Limit)
            counter.Value = Limit;
        else
            counter.Value += inc;
    }

    public void Decrement(ICounter counter)
    {
        if (!CanDecrement(counter)) return;
        Oops();

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