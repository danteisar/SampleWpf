using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SamplePrism2025.Shared.Abstractions;

public interface IValueModel : INotifyPropertyChanged
{
    int Value { get; set; }
    ObservableCollection<int> ValuesList { get; set; }
}