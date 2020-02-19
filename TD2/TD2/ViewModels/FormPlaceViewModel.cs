using System;
using System.IO;
using System.Windows.Input;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Storm.Mvvm;
using Storm.Mvvm.Services;
using TD2.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TD2.ViewModels
{
    public class FormPlaceViewModel : ViewModelBase
    {
        private string Id { get; set; }

        private string _image;
        public string Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }
        
        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        
        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        
        private string _latitude;
        public string Latitude
        {
            get => _latitude;
            set => SetProperty(ref _latitude, value);
        }
        
        private string _longitude;
        public string Longitude
        {
            get => _longitude;
            set => SetProperty(ref _longitude, value);
        }

        public string YourPositionText { get; }
        public ICommand YourPositionCommand { get; set; }
        
        public string AddPlaceText { get; }
        public ICommand AddPlaceCommand { get; set; }
        
        public string ChooseImageText { get; }
        public ICommand ChooseImageCommand { get; set; }
        
        public FormPlaceViewModel()
        {
            YourPositionText = "Your position";
            AddPlaceText = "Add place";
            ChooseImageText = "Choose an image";
            YourPositionCommand = new Command(GetPosition);
            AddPlaceCommand = new Command(AddPlace);
            ChooseImageCommand = new Command(SetImage);
        }
        
        private async void AddPlace()
        {
            if (Id != null && Convert.ToInt32(Id) != 0)
            {
                ApiService.AddPlace(Title, Description, Convert.ToDouble(Latitude), Convert.ToDouble(Longitude), Convert.ToInt32(Id));
            }
            else
            {
                var answer = await AlertService.ConfirmationAction();
                if (answer)
                {
                    ApiService.AddPlace(Title, Description, double.Parse(Latitude), double.Parse(Longitude), 1);
                }
            }
        }
        
        private async void SetImage()
        {
            (Image, Id) = await ImageService.SetImage();
        }
        
        private async void GetPosition()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);
                //var location = await Geolocation.GetLastKnownLocationAsync();
                Latitude = location.Latitude.ToString();
                Longitude = location.Longitude.ToString();
            }
            catch (FeatureNotSupportedException)
            {
                // Handle not supported on device exception
                AlertService.Error("Your device does not support location", false);
            }
            catch (FeatureNotEnabledException)
            {
                // Handle not enabled on device exception
                AlertService.Error("Location not activate", false);
            }
            catch (PermissionException)
            {
                // Handle permission exception
                AlertService.Error("You did not allow the application to access the location", false);
            }
            catch (Exception)
            {
                // Unable to get location
                AlertService.Error("Error", false);
            }
        }
        
    }
}