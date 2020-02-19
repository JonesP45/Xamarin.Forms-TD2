using System.Threading.Tasks;
using System.Windows.Input;
using Storm.Mvvm;
using TD2.Models;
using TD2.Ressources;
using Xamarin.Forms;

namespace TD2.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
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

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _urlImage;
        public string UrlImage
        {
            get => _urlImage;
            set => SetProperty(ref _urlImage, value);
        }
        
        public string SignInText { get; }
        public ICommand SignInCommand { get; set; }
        
        public string SignUpText { get; }
        public ICommand SignUpCommand { get; set; }
        
        public string TextEditProfileButton { get; }
        public ICommand GoToEditProfileCommand { get; set; }

        public ProfileViewModel()
        {
            TextEditProfileButton = "Edit";
            SignInText = "Sign in";
            SignUpText = "Sign up";
            var goToService = new GoToService();
            SignInCommand = new Command(goToService.GoToSignIn);
            SignUpCommand = new Command(goToService.GoToSignUp);
            GoToEditProfileCommand = new Command(GoToEdit);
        }
        
        public override async Task OnResume()
        {
            await base.OnResume();
            if (!UserService.IsConnected())
            {
                AlertService.UserNotConnected();
            }
            else
            {
                var userItem = await ApiService.Me();
                Firstname = userItem.Data.FirstName;
                Lastname = userItem.Data.LastName;
                Email = userItem.Data.Email;
                UrlImage = Urls.URI + Urls.GET_IMAGE + userItem.Data.ImageId;
            }
        }

        private void GoToEdit()
        {
            AlertService.EditProfile(Firstname, Lastname, UrlImage);
        }
        
    }
}