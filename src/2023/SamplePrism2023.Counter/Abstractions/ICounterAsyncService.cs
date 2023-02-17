using System.Threading.Tasks;

namespace SamplePrism2023.Counter.Abstractions;

/// <summary>
///     Бизнес-логика счетчика
/// </summary>
public interface ICounterAsyncService
{
    /// <summary>
    ///     Увеличить значение
    /// </summary>
    /// <param name="counter"></param>
    Task Increment(ICounter counter);

    /// <summary>
    ///     Уменьшить значение
    /// </summary>
    /// <param name="counter"></param>
    Task Decrement(ICounter counter);

    /// <summary>
    ///     Можно ли дальше увеличивать значение
    /// </summary>
    /// <param name="counter"></param>
    /// <returns></returns>
    bool CanIncrement(ICounter counter);

    /// <summary>
    ///     Можно ли дальше уменьшать значение
    /// </summary>
    /// <param name="counter"></param>
    /// <returns></returns>
    bool CanDecrement(ICounter counter);
}