using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace MVVM.Extensions;

public static class BindableBaseExtensions
{
    private class MyDisposable<TObj, TProp> : IDisposable
        where TObj : INotifyPropertyChanged
    {
        public MyDisposable(Action handler,
            TObj source,
            string propertyName)
        {
            Source = source;
            Handler = (_, e) =>
            {
                if (e.PropertyName != propertyName) return;
                handler();
            };
            Source.PropertyChanged += Handler;
        }

        public MyDisposable(Action<TProp> handler,
            TObj source,
            Func<TObj, TProp> selector,
            string propertyName)
        {
            Source = source;
            Handler = (s, e) =>
            {
                if (e.PropertyName != propertyName) return;
                handler(selector((TObj)s));
            };
            Source.PropertyChanged += Handler;
        }

        private PropertyChangedEventHandler Handler { get; }
        private TObj Source { get; }

        public void Dispose()
        {
            Source.PropertyChanged -= Handler;
        }
    }

    /// <summary>
    /// Subscribe on property changes for the specified property.
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="sender">The object implements <see cref="INotifyPropertyChanged"./></param>
    /// <param name="propertyAccessor">The property to subscribe.</param>
    /// <param name="action">Callback</param>
    /// <returns><see cref="IDisposable"/></returns>
    public static IDisposable WhenPropertyChanged<TObject, TProperty>(this TObject sender,
        Expression<Func<TObject, TProperty>> propertyAccessor, Action<TProperty> action)
        where TObject : INotifyPropertyChanged
    {
        var member = propertyAccessor.Body as MemberExpression;
        var propInfo = member?.Member as PropertyInfo;
        return new MyDisposable<TObject, TProperty>(action, sender, propertyAccessor.Compile(), propInfo?.Name);
    }

    /// <summary>
    /// Subscribe on property changes for the specified property.
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="sender">The object implements <see cref="INotifyPropertyChanged"./></param>
    /// <param name="propertyAccessor">The property to subscribe.</param>
    /// <param name="action">Callback</param>
    /// <returns><see cref="IDisposable"/></returns>
    public static IDisposable WhenPropertyChanged<TObject, TProperty>(this TObject sender,
        Expression<Func<TObject, TProperty>> propertyAccessor, Action action)
        where TObject : INotifyPropertyChanged
    {
        var member = propertyAccessor.Body as MemberExpression;
        var propInfo = member?.Member as PropertyInfo;
        return new MyDisposable<TObject, TProperty>(action, sender, propInfo?.Name);
    }
}