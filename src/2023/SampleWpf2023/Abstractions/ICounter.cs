using System.ComponentModel;

namespace SampleWpf2023.Abstractions;

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