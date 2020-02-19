﻿using System;
using TD2.Ressources;

namespace TD2.Models
{
    public static class UserService
    {
        public static LoginResult LoginResult { get; private set; }
        private static DateTime DateTimeSignIn { get; set; }

        public static string GetAccessToken()
        {
            if (!IsConnected())
            {
                return null;
            }
            if (!AccessTokenValid())
                ApiService.RefreshAccessToken();
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