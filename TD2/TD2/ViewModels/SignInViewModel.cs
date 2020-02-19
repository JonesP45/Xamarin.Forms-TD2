using System.Windows.Input;
using Storm.Mvvm;
using TD2.Models;
using Xamarin.Forms;

namespace TD2.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {
        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
        
        public string SignInText { get; }
        
        public ICommand SignInCommand { get; }

        public SignInViewModel()
        {
            SignInText = "Sign in";
            SignInCommand = new Command(SignIn);
        }

        private void SignIn()
        {
            ApiService.SignIn(Email, Password);
        }

    }
}