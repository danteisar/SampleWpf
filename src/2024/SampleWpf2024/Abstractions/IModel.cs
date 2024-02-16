using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SampleWpf2024.Abstractions;

public interface IModel : INotifyPropertyChanged
{
    int Input { get; set; }
    ObservableCollection<int> History { get; }
}
