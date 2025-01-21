using MVVM.Commands;
using MVVM.Extensions;
using ReactiveUI;
using SampleWpf2024.Abstractions;
using SampleWpf2024.Models;
using SampleWpf2024.Services;
using System.Collections.Specialized;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Windows.Input;

namespace SampleWpf2024.ViewModels
{
    internal class ViewModel
    {
        public IModel Model { get; } = new Model();
        public ServiceModel Service { get; } = new();

        public ViewModel()
        {
            Model.WhenPropertyChanged(x => x.Input, CheckCommands);
            Model.History.CollectionChanged += HistoryOnCollectionChanged;
            Clear = new RelayCommand(() => Service.Clear(Model), () => Model.History.Any());
            Pop = new RelayCommand(() => Service.Pop(Model), Test);

            // Реактивное программирование
            var inputObs = Model.WhenAnyValue(x => x.Input);
            var boolObs = inputObs.Select(x => x != 0);
            Push = ReactiveCommand.CreateFromTask(() => Service.Push(Model), boolObs);
            Rand = ReactiveCommand.Create(() => Service.Random(Model));
            Observable.Interval(TimeSpan.FromMilliseconds(500))
                .ObserveOn(DispatcherScheduler.Current)
                .Skip(2)
                .Take(5)
                .Subscribe(x => Model.Input = (int)x);
        }

        public ICommand Clear { get; set; }
        public ICommand Push { get; set; }
        public ICommand Pop { get; set; }
        public ICommand Rand { get; set; }

        private bool Test()
        {
            return Model.Input == 0 && Model.History.Any();
        }

        private void HistoryOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Clear.RaiseCanExecuteChanged();
        }

        private void CheckCommands()
        {
            Pop.RaiseCanExecuteChanged();
        }
    }
}
