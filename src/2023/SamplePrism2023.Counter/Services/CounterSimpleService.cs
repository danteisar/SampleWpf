using SamplePrism2023.Counter.Abstractions;

namespace SamplePrism2023.Counter.Services;

internal class CounterSimpleService : ICounterService
{
    private const int Limit = 5;
    
    public void Increment(ICounter counter)
    {
        if (!CanIncrement(counter)) return;
        counter.Value++;
    }

    public void Decrement(ICounter counter)
    {
        if (!CanDecrement(counter)) return;
        counter.Value--;
    }

    public bool CanIncrement(ICounter counter)
        => counter.Value < Limit;

    public bool CanDecrement(ICounter counter)
        => counter.Value >= 0;
}