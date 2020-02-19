using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TD.Api.Dtos;
using TD2.Ressources;

namespace TD2.Models
{
    public static class ApiService
    {
        private static async Task<HttpResponseMessage> GetResponse(HttpMethod method, string url, object data)
        {
            try
            {
                var apiClient = new ApiClient();
                return await apiClient.Execute(method, url, data, UserService.GetAccessToken());
            }
            catch (Exception)
            {
                AlertService.Error("Problem", true);
                return null;
            }
            
        }
        
        public static async void GetPlaces()
        {
            var apiClient = new ApiClient();
            var method = HttpMethod.Get;
            const string url = Urls.URI + Urls.LIST_PLACES;
            var response = await GetResponse(method, url, null);
            if (response == null) return;
            if (response.IsSuccessStatusCode)
            {
                var result = await apiClient.ReadFromResponse<Response<List<PlaceItemSummary>>>(response);
                foreach (var placeItem in result.Data)
                {
                    var place = new Place(placeItem);
                    PlaceList.AddPlace(place);
                }
            }
            else
            {
                AlertService.Error(response.StatusCode.ToString(), false);
            }
        }
        
        public static async Task<ObservableCollection<CommentItem>> GetComments(int placeId)
        {
            var apiClient = new ApiClient();
            var method = HttpMethod.Get;
            var url = Urls.URI + Urls.GET_PLACE + placeId;
            var response = await GetResponse(method, url, null);
            if (response == null) return null;
            if (response.IsSuccessStatusCode)
            {
                CommentList.ClearAll();
                var result = await apiClient.ReadFromResponse<Response<PlaceItem>>(response);
                foreach (var commentItem in result.Data.Comments)
                {
                    CommentList.AddComment(commentItem);
                }
                return CommentList.Comments;
            }
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                AlertService.Error(response.StatusCode.ToString(), true);
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                AlertService.Error(response.StatusCode.ToString(), true);
            }
            return null;
        }

        public static async void AddComment(int id, string textAddComment)
        {
            var method = HttpMethod.Post;
            var url = Urls.URI + Urls.GET_PLACE + id + Urls.CREATE_COMMENT;
            var body = new CreateCommentRequest {Text = textAddComment};
            var response = await GetResponse(method, url, body);
            if (response == null) return;
            if (response.IsSuccessStatusCode)
            {
                new GoToService().GoToBackPage();
            }
            else
            {
                AlertService.Error(response.StatusCode.ToString(), false);
            }
        }
        
        public static async Task<Response<UserItem>> Me()
        {
            var apiClient = new ApiClient();
            var method = HttpMethod.Get;
            const string url = Urls.URI + Urls.ME;
            var response = await GetResponse(method, url, null);
            if (response == null) return null;
            if (response.IsSuccessStatusCode)
            {
                var result = await apiClient.ReadFromResponse<Response<UserItem>>(response);
                return result;
            }
            AlertService.Error(response.StatusCode.ToString() + response.Content, false);
            return null;
        }
        
        public static async void SignIn(string email, string password)
        {
            var apiClient = new ApiClient();
            var method = HttpMethod.Post;
            const string url = Urls.URI + Urls.LOGIN;
            var body = new LoginRequest {Email = email, Password = password};
            var response = await apiClient.Execute(method, url, body, UserService.GetAccessToken());
            // var response = await GetResponse(method, url, body);
            if (response == null) return;
            if (response.IsSuccessStatusCode)
            {
                var result = await apiClient.ReadFromResponse<Response<LoginResult>>(response);
                UserService.Connexion(result.Data);
                // var parameters = new Dictionary<string, object>();
                // parameters.Add("LoginResult", result.Data);
                // await NavigationService.PushAsync<PlacesPage>(parameters);
                new GoToService().GoToBackPage();
                // await NavigationService.PopAsync();
            }
            else
            {
                // var result = DependencyService.Resolve<IDialogService>();
                AlertService.Error("Wrong email and/or password", false);
                // await result.DisplayAlertAsync("Attention?", "Wrong email and/or password", "OK");
            }
        }
        
        public static async void SignUp(string email, string firstname, string lastname, string password)
        {
            var apiClient = new ApiClient();
            var method = HttpMethod.Post;
            const string url = Urls.URI + Urls.REGISTER;
            var body = new RegisterRequest
            {
                Email = email, FirstName = firstname, LastName = lastname, Password = password
            };
            // var response = await apiClient.Execute(method, url, body, UserService.GetAccessToken());
            var response = await GetResponse(method, url, body);
            if (response == null) return;
            if (response.IsSuccessStatusCode)
            {
                var result = await apiClient.ReadFromResponse<Response<LoginResult>>(response);
                UserService.Connexion(result.Data);
                new GoToService().GoToBackPage();
            }
            else
            {
                AlertService.Error(response.StatusCode.ToString(), false);
            }
        }

        public static async Task<string> PostImage(byte[] imageData)
        {
            var client = new HttpClient();
            var method = HttpMethod.Post;
            const string url = Urls.URI + Urls.CREATE_IMAGE;
            var request = new HttpRequestMessage(method, url);
            var token = UserService.GetAccessToken();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var requestContent = new MultipartFormDataContent();

            var imageContent = new ByteArrayContent(imageData);
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");

            requestContent.Add(imageContent, "file", "file.jpg");

            request.Content = requestContent;

            var response = await client.SendAsync(request);
            if (response == null) return null;

            var result = await response.Content.ReadAsStringAsync();
            var id = JsonConvert.DeserializeObject<Response<ImageItem>>(result);

            if (response.IsSuccessStatusCode)
            {
                AlertService.Info("Image upload", false);
            }
            else if (response.StatusCode == HttpStatusCode.RequestEntityTooLarge)
            {
                AlertService.Error(response.StatusCode.ToString(), false);
            }
            else
            {
                AlertService.Error("Image not upload", false);
            }
            return id.Data.Id.ToString();
        }
        
        public static async void AddPlace(string title, string description, double latitude, double longitude, int imageId)
        {
            var method = HttpMethod.Post;
            const string url = Urls.URI + Urls.CREATE_PLACE;
            var body = new CreatePlaceRequest
            {
                Title = title,
                Description = description,
                Latitude = latitude,
                Longitude = longitude,
                ImageId = imageId
            };
            // var response = await apiClient.Execute(method, url, body, UserService.GetAccessToken());
            var response = await GetResponse(method, url, body);
            if (response == null) return;
            if (response.IsSuccessStatusCode)
            {
                new GoToService().GoToBackPage();
            }
            else
            {
                AlertService.Error(response.StatusCode.ToString(), false);
            }
        }
        
        public static async void EditPassword(string oldPassword, string newPassword)
        {
            var method = new HttpMethod("PATCH");
            const string url = Urls.URI + Urls.UPDATE_PASSWORD;
            var body = new UpdatePasswordRequest {OldPassword = oldPassword, NewPassword = newPassword};
            // var response = await apiClient.Execute(method, url, body, UserService.GetAccessToken());
            var response = await GetResponse(method, url, body);
            if (response == null) return;
            if (response.IsSuccessStatusCode)
            {
                new GoToService().GoToBackPage();
            }
            else
            {
                AlertService.Error(response.StatusCode.ToString(), false);
            }
        }
        
        public static async void EditProfile(string firstname, string lastname, int id)
        {
            var method = new HttpMethod("PATCH");
            const string url = Urls.URI + Urls.UPDATE_PROFILE;
            var body = new UpdateProfileRequest {FirstName = firstname, LastName = lastname, ImageId = id};
            // var response = await apiClient.Execute(method, url, body, UserService.GetAccessToken());
            var response = await GetResponse(method, url, body);
            if (response == null) return;
            if (response.IsSuccessStatusCode)
            {
                new GoToService().GoToBackPage();
            }
            else
            {
                AlertService.Error(response.StatusCode.ToString(), false);
            }
        }
        
    }
}