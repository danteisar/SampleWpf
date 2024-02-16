using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SamplePrism2024.Showcase.Abstractions;

public interface IModel : INotifyPropertyChanged
{
    int Input { get; set; }
    ObservableCollection<int> History { get; }
}
