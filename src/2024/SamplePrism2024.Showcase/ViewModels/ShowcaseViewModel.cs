using System.Collections.Specialized;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Windows.Input;
using MVVM.Commands;
using MVVM.Extensions;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SamplePrism2024.Shared;
using SamplePrism2024.Shared.Events;
using SamplePrism2024.Shared.Extensions;
using SamplePrism2024.Showcase.Abstractions;

namespace SamplePrism2024.Showcase.ViewModels
{
    internal class ShowcaseViewModel : ReactiveObject
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;
       
        public IModel Model { get; } 
        public IServiceModel Service { get; }

        [Reactive] public bool CanShowAbout { get; set; } = true;

        public ShowcaseViewModel(IEventAggregator eventAggregator, IRegionManager regionManager, IModel model, IServiceModel service)
        {
            //inject _fields & Props
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            Model = model;
            Service = service;

            // MVVM lib observe property for changing
            Model.WhenPropertyChanged(x => x.Input, OnInputChanged);
            Model.History.CollectionChanged += HistoryOnCollectionChanged;

            // MVVM lib commands samples
            Clear = new RelayCommand(ExecuteClear, Model.History.Any);
            Pop = new RelayCommand(ExecutePop, CanPop);
            
            // Prism command sample
            About = new DelegateCommand(OpenAbout);

            // Reactive UI usage
            var inputObs = Model.WhenAnyValue(x => x.Input);
            var boolObs = inputObs.Select(x => x.HasValue);
            Push = ReactiveCommand.CreateFromTask(() => Service.Push(Model), boolObs);
            Rand = ReactiveCommand.Create(() => Service.Random(Model));

            // Observable sequence sample
            var t = new Random();
            Observable.Interval(TimeSpan.FromMilliseconds(100))
                .ObserveOn(DispatcherScheduler.Current)
                .Skip(1)
                .Take(10)
                .Subscribe(x => Model.Input = t.Next(-9, 9));

            // Observe for property
            Model.History
                .ObservableForProperty(x=>x.Count)
                .Throttle(TimeSpan.FromMilliseconds(100))
                .Select(x=>x.Value == 0)
                .ToProperty(this, x=>x.CanShowAbout)
                .Caсhe();
        }

        public ICommand Clear { get; set; }
        public ICommand Push { get; set; }
        public ICommand Pop { get; set; }
        public ICommand Rand { get; set; }
        public ICommand About { get; set; }

        private void ExecutePop() => Service.Pop(Model);
        private bool CanPop() => Service.Check(Model);
        private void ExecuteClear() => Service.Clear(Model);
        private void OpenAbout() => _regionManager.RequestNavigate(Regions.MainRegion, Navigation.AboutPage);

        /// <summary>
        /// Check commands
        /// </summary>
        private void HistoryOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Clear.RaiseCanExecuteChanged();
            Pop.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Event aggregator && check pop command
        /// </summary>
        private void OnInputChanged()
        {
            Pop.RaiseCanExecuteChanged();
            
            _eventAggregator
                .GetEvent<InputChanged>()
                .Publish(Model.Input);
        }
    }
}
