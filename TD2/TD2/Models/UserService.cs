using System;
using System.Net.Http;
using TD2.Ressources;

namespace TD2.Models
{
    public static class UserService
    {
        private static LoginResult LoginResult { get; set; }
        private static DateTime DateTimeSignIn { get; set; }

        private static async void RefreshAccessToken()
        {
            var apiClient = new ApiClient();
            var method = HttpMethod.Post;
            const string url = Urls.URI + Urls.REFRESH;
            var body = new RefreshRequest {RefreshToken = LoginResult.RefreshToken};
            var response = await apiClient.Execute(method, url, body);
            if (response.IsSuccessStatusCode)
            {
                var result = await apiClient.ReadFromResponse<Response<LoginResult>>(response);
                Connexion(result.Data);
            }
        }

        public static string GetAccessToken()
        {
            if (!IsConnected())
            {
                return null;
            }
            if (!AccessTokenValid())
                RefreshAccessToken();
            return LoginResult.AccessToken;
        }

        public static void Connexion(LoginResult loginResult)
        {
            LoginResult = loginResult;
            DateTimeSignIn = DateTime.Now;
        }

        private static bool AccessTokenValid()
        {
            return DateTime.Now.Second < DateTimeSignIn.Second + LoginResult.ExpiresIn;
        }

        public static bool IsConnected()
        {
            return LoginResult != null;
        }

    }
}