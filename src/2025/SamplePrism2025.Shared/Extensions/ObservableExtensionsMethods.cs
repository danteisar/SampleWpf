using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace SamplePrism2025.Shared.Extensions;

public static class ObservableExtensionsMethods
{

    private static readonly ICollection<IDisposable> Disposables = new List<IDisposable>();

    public static IDisposable Caсhe(this IDisposable disposable)
    {
        Disposables.Add(disposable);
        return disposable;
    }

    public static IObservable<T> ToObservable<T>(this PubSubEvent<T> pubSubEvent)
    {
        return Observable.Create<T>(obs => pubSubEvent.Subscribe(obs.OnNext));
    }

    public static IObservable<Unit> ToObservable(this PubSubEvent pubSubEvent)
    {
        return Observable.FromEvent
        (
            act => pubSubEvent.Subscribe(act),
            pubSubEvent.Unsubscribe
        );
    }

    public static IObservable<bool> IsNotNull<T>(this IObservable<T> observable)
    where T : class
    {
        return observable.Select(x => x is not null);
    }

    public static IObservable<bool> IsNull<T>(this IObservable<T> observable)
    where T : class
    {
        return observable.Select(x => x is null);
    }

    public static IObservable<Unit> GetInitializationObs()
    {
        return Observable.Return(Unit.Default);
    }

    public static IObservable<Task<Unit>> ToUnit(this IObservable<Task> taskObs)
    {
        return taskObs.Select(async t =>
        {
            await t;
            return Unit.Default;
        });
    }

    public static IObservable<Unit> WithInitialization(this IObservable<Unit> obs)
    {
        return obs.Merge(GetInitializationObs());
    }

    public static IObservable<Unit> WithInitialization<T>(this IObservable<T> obs)
    {
        return obs.Select(_ => Unit.Default)
                  .WithInitialization();
    }

    public static IObservable<T> DelayFirst<T>(this IObservable<T> obs, TimeSpan timeSpan)
    {
        return obs.Take(1)
                  .Delay(timeSpan)
                  .Merge(obs.Skip(1));
    }

    public static IObservable<T> Throttle<T>(this IObservable<T> obs)
    {
        return obs.Throttle(TimeSpan.FromMilliseconds(100));
    }

    /// <summary>
    /// Когда последовательность завершается, буффер освобождается и все сигналы высвобождаются, поэтому с использованием Observable.Return ленивый механизм не работает. Необходима теоретически бесконечная последовательность
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obs"></param>
    /// <param name="delayedObs"></param>
    /// <returns></returns>
    public static IObservable<T> DelayedBy<T>(this IObservable<T> obs, IObservable<bool> delayedObs, int capacity = 31)
    {
        var trueObs = obs.WithLatestFrom(delayedObs)
                         .Where(x => x.Second)
                         .Select(x => x.First);

        var falseObs = obs.WithLatestFrom(delayedObs.Merge(Observable.Return(false)))
                          .Where(x => !x.Second)
                          .Select(x => x.First)
                          .Window(() => delayedObs.Where(x => x))
                          .SelectMany(window => window.Aggregate(new List<T>(),
                          (list, item) =>
                          {
                              list.Add(item);

                              if (list.Count > capacity)
                              {
                                  list.RemoveAt(0);
                              }

                              return list;
                          })).SelectMany(x => x);

        return trueObs.Merge(falseObs);
    }

    public static IObservable<TOut> SelectTask<TIn, TOut>(this IObservable<TIn> observable, Func<TIn, Task<TOut>> func, bool observeOnDispatcher = true)
    {
        var result = observable.Select(func)
                               .Concat();

        if (observeOnDispatcher)
        {
            result = result.ObserveOn(DispatcherScheduler.Current);
        }

        return result;
    }
}
