using System;
using System.Collections.Generic;
using System.Windows.Input;
using Storm.Mvvm;
using TD2.Models;
using Xamarin.Forms;

namespace TD2.ViewModels
{
    public class FormProfileViewModel : ViewModelBase
    {
        // private byte[] ImageData { get; set; }
        // private string Path { get; set; }
        // private string APpath { get; set; }
        private string Id { get; set; }
        
        private string _image;
        public string Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        private string _firstname;
        public string Firstname
        {
            get => _firstname;
            set => SetProperty(ref _firstname, value);
        }
        
        private string _lastname;
        public string Lastname
        {
            get => _lastname;
            set => SetProperty(ref _lastname, value);
        }

        public string EditText { get; }
        public ICommand EditCommand { get; set; }
        
        public string ChooseImageText { get; }
        public ICommand ChooseImageCommand { get; set; }
        
        public FormProfileViewModel()
        {
            EditText = "Edit";
            ChooseImageText = "Choose image";
            EditCommand = new Command(Edit);
            ChooseImageCommand = new Command(SetImage);
        }

        public override void Initialize(Dictionary<string, object> navigationParameters)
        {
            base.Initialize(navigationParameters);
            Firstname = GetNavigationParameter<string>("Firstname");
            Lastname = GetNavigationParameter<string>("Lastname");
            Image = GetNavigationParameter<string>("UrlImage");
        }

        private void Edit()
        {
            ApiService.EditProfile(Firstname, Lastname, Convert.ToInt32(Id));
        }

        private async void SetImage()
        {
            (Image, Id) = await ImageService.SetImage();
        }

    }
}