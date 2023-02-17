using System.ComponentModel;

namespace SamplePrism2023.Counter.Abstractions;

/// <summary>
///     Модель счетчика
/// </summary>
public interface ICounter : INotifyPropertyChanged
{
    /// <summary>   
    ///     Значение счетчика
    /// </summary>
    int Value { get; set; }
}