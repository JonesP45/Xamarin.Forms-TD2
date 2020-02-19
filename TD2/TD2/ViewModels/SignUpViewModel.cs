using System.Windows.Input;
using Storm.Mvvm;
using TD2.Models;
using Xamarin.Forms;

namespace TD2.ViewModels
{
    public class SignUpViewModel : ViewModelBase
    {
        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
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

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
        
        public string SignUpText { get; }
        
        public ICommand SignUpCommand { get; }
        
        public SignUpViewModel()
        {
            SignUpText = "Sign up";
            SignUpCommand = new Command(SignUp);
        }

        private void SignUp()
        {
            ApiService.SignUp(Email, Firstname, Lastname, Password);
        }

    }
}