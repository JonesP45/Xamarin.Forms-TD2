using System.Collections.Generic;
using Storm.Mvvm;
using TD2.Views;

namespace TD2.Models
{
    public class GoToService : ViewModelBase
    {
        public async void GoToSignIn()
        {
            await NavigationService.PushAsync<SignInPage>();
        }
        
        public async void GoToSignUp()
        {
            await NavigationService.PushAsync<SignUpPage>();
        }
        
        public async void GoToEditProfile(string firstname, string lastname, string urlImage)
        {
            var parameters = new Dictionary<string, object> {{"Firstname", firstname}, {"Lastname", lastname}, {"UrlImage", urlImage}};
            await NavigationService.PushAsync<FormProfilePage>(parameters);
        }
        
        public async void GoToEditPassword()
        {
            await NavigationService.PushAsync<FormPasswordPage>();
        }
        
        public async void GoToBackPage()
        {
            await NavigationService.PopAsync();
        }
        
        public async void GoToFormPlace()
        {
            await NavigationService.PushAsync<FormPlacePage>();
        }

        public async void GoToDetailPlace(Place place)
        {
            if (place != null)
            {
                var parameters = new Dictionary<string, object> {{"PlaceItemSummary", place.PlaceItem}};
                await NavigationService.PushAsync<DetailPlacePage>(parameters);
            }
        }

        public async void GoToProfile(object _)
        {
            await NavigationService.PushAsync<ProfilePage>();
        }
        
        public async void GoToComments(Dictionary<string, object> parameters)
        {
            await NavigationService.PushAsync<CommentsPage>(parameters);
        }
        
        public async void GoToFormComment(Dictionary<string, object> parameters)
        {
            await NavigationService.PushAsync<FormCommentPage>(parameters);
        }
    }
}