using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Storm.Mvvm;
using TD2.Models;
using TD2.Ressources;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TD2.ViewModels
{
    public class DetailPlaceViewModel : ViewModelBase
    {
        private PlaceItemSummary PlaceItemSummary { get; set; }

        private Position _myPosition;
        public Position MyPosition
        {
            get => _myPosition;
            set => SetProperty(ref _myPosition, value);
        }
        
        private ObservableCollection<Pin> _pinCollection = new ObservableCollection<Pin>();
        public ObservableCollection<Pin> PinCollection
        {
            get => _pinCollection;
            set => SetProperty(ref _pinCollection, value);
        }
        
        public string Comments { get; }
        public ICommand CommentsCommand { get; set; }

        public DetailPlaceViewModel()
        {
            PinCollection = new ObservableCollection<Pin>();
            CommentsCommand = new Command(GoToComments);
            Comments = "See comments";
        }

        public override void Initialize(Dictionary<string, object> navigationParameters)
        {
            base.Initialize(navigationParameters);
            PlaceItemSummary = GetNavigationParameter<PlaceItemSummary>("PlaceItemSummary");
            MyPosition = new Position(PlaceItemSummary.Latitude, PlaceItemSummary.Longitude);
            PinCollection.Add(new Pin()
            {
                Position = MyPosition, Type = PinType.Generic, Label = PlaceItemSummary.Title
            });
        }
        
        private void GoToComments()
        {
            var parameters = new Dictionary<string, object> {{"Id", PlaceItemSummary.Id}};
            new GoToService().GoToComments(parameters);
        }

    }
}