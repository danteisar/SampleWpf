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
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            Model = model;
            Service = service;

            Model.WhenPropertyChanged(x => x.Input, OnInputChanged);
            Model.History.CollectionChanged += HistoryOnCollectionChanged;

            //MVVM lib commands
            Clear = new RelayCommand(ExecuteClear, Model.History.Any);
            Pop = new RelayCommand(ExecutePop, CanPop);
            
            //Prism command
            About = new DelegateCommand(OpenAbout);

            // Реактивное программирование
            var inputObs = Model.WhenAnyValue(x => x.Input);
            var boolObs = inputObs.Select(x => x != 0);
            Push = ReactiveCommand.CreateFromTask(() => Service.Push(Model), boolObs);
            Rand = ReactiveCommand.Create(() => Service.Random(Model));

            var t = new Random();
            Observable.Interval(TimeSpan.FromMilliseconds(100))
                .ObserveOn(DispatcherScheduler.Current)
                .Skip(1)
                .Take(10)
                .Subscribe(x => Model.Input = t.Next(-9, 9));

            Model.History
                .ObservableForProperty(x=>x.Count)
                .Select(x=>x.Value)
                .Subscribe(CheckCount);
        }

        private void CheckCount(int x)
        {
            CanShowAbout = x == 0;
        }

        public ICommand Clear { get; set; }
        public ICommand Push { get; set; }
        public ICommand Pop { get; set; }
        public ICommand Rand { get; set; }
        public ICommand About { get; set; }

        private void ExecutePop() => Service.Pop(Model);
        private bool CanPop() => Model.Input == 0 && Model.History.Any();

        private void ExecuteClear() => Service.Clear(Model);
        private void OpenAbout() => _regionManager.RequestNavigate(Regions.MainRegion, Navigation.AboutPage);

        private void HistoryOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Clear.RaiseCanExecuteChanged();
            Pop.RaiseCanExecuteChanged();
        }

        private void OnInputChanged()
        {
            Pop.RaiseCanExecuteChanged();
            
            _eventAggregator
                .GetEvent<InputChanged>()
                .Publish(Model.Input);
        }
    }
}
