using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Windows.Input;
using Storm.Mvvm;
using TD2.Ressources;
using TD2.Views;
using Xamarin.Forms;

namespace TD2.Models
{
    public class Place : ViewModelBase
    {
        private PlaceItemSummary _placeItem;
        private string _urlImage;

        // public ICommand DetailCommand { get; set; }

        public Place(PlaceItemSummary placeItem)
        {
            PlaceItem = placeItem;
            UrlImage = Urls.URI + Urls.GET_IMAGE + PlaceItem.ImageId;
            // DetailCommand = new Command(GoToDetailPace);
        }

        // private async void GoToDetailPace(object _)
        // {
        //     await NavigationService.PushAsync<DetailPlacePage>();
        // }

        public PlaceItemSummary PlaceItem
        {
            get => _placeItem;
            set => SetProperty(ref _placeItem, value);
        }
        
        public string UrlImage
        {
            get => _urlImage;
            set => SetProperty(ref _urlImage, value);
        }
        
    }
}