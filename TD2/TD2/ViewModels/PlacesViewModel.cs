using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Storm.Mvvm;
using TD2.Models;
using Xamarin.Forms;

namespace TD2.ViewModels
{
    public class PlacesViewModel : ViewModelBase
    {
        private readonly GoToService _goToService = new GoToService();

        private ObservableCollection<Place> _places;
        public ObservableCollection<Place> Places
        {
            get => _places;
            set => SetProperty(ref _places, value);
        }

        private Place _detailPlaceSelected;
        public Place DetailPlaceSelected
        {
            get => _detailPlaceSelected;
            set
            {
                if (SetProperty(ref _detailPlaceSelected, value) && value != null)
                {
                    _goToService.GoToDetailPlace(value);
                    DetailPlaceSelected = null;
                }
            }
        }

        public string TextProfileButton { get; }
        public ICommand GoToProfileCommand { get; set; }
        
        public ICommand GoToFormPlaceCommand { get; set; }

        public PlacesViewModel()
        {
            TextProfileButton = "Profile";
            GoToProfileCommand = new Command(_goToService.GoToProfile);
            GoToFormPlaceCommand = new Command(GoToFormPlace);
        }
        
        public override async Task OnResume()
        {
            await base.OnResume();
            ApiService.GetPlaces();
            Places = PlaceList.Places;
        }
        
        private void GoToFormPlace()
        {
            if (UserService.IsConnected())
            {
                _goToService.GoToFormPlace();
            }
            else
            {
                AlertService.UserNotConnected();
            }
        }

    }
}