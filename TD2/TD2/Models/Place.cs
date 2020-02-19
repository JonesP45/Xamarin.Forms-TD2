using Storm.Mvvm;
using TD2.Ressources;

namespace TD2.Models
{
    public class Place : ViewModelBase
    {
        private PlaceItemSummary _placeItem;
        private string _urlImage;
        
        public Place(PlaceItemSummary placeItem)
        {
            PlaceItem = placeItem;
            UrlImage = Urls.URI + Urls.GET_IMAGE + PlaceItem.ImageId;
        }

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