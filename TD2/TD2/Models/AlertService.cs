using System.Threading.Tasks;
using Storm.Mvvm.Services;
using Xamarin.Forms;

namespace TD2.Models
{
    public static class AlertService
    {
        private static readonly GoToService GoToService = new GoToService();

        public static async Task<bool> ConfirmationAction()
        {
            var result = DependencyService.Resolve<IDialogService>();
            var answer = await result.DisplayAlertAsync("Attention", "Photo cannot be updated. Would you like to continue with a random photo?", "Yes", "No");
            return answer;
        }
        
        public static async void UserNotConnected()
        {
            var result = DependencyService.Resolve<IDialogService>();
            const string actionSignIn = "SignIn";
            const string actionSignUp = "SignUp";
            var answer = await result.DisplayActionSheet("Not connected\nWould you like to:", "Cancel", null, actionSignIn, actionSignUp);
            if (answer.Equals(actionSignIn))
            {
                GoToService.GoToSignIn();
            }
            else if (answer.Equals(actionSignUp))
            {
                GoToService.GoToSignUp();
            }
        }
        
        public static async void EditProfile(string firstname, string lastname, string urlImage)
        {
            var result = DependencyService.Resolve<IDialogService>();
            const string actionProfile = "Profile";
            const string actionPassword = "Password";
            var answer = await result.DisplayActionSheet("Would you like to edit:", "Cancel", null, actionProfile, actionPassword);
            if (answer.Equals(actionPassword))
            {
                GoToService.GoToEditPassword();
            }
            else if (answer.Equals(actionProfile))
            {
                GoToService.GoToEditProfile(firstname, lastname, urlImage);
            }
        }
        
        public static async void Error(string message, bool back)
        {
            var result = DependencyService.Resolve<IDialogService>();
            await result.DisplayAlertAsync("Error", message, "Ok");
            if (back)
                GoToService.GoToBackPage();
        }
    }
}