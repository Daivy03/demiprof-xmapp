using System;
using Android.Content;
using Android.Gms.Auth.Api;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using DemiProf.Droid;
using DemiProf.Models;
using UventoXF.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(GoogleManager))]
namespace DemiProf.Droid
{
    public class GoogleManager : Java.Lang.Object, IGoogleManager, GoogleApiClient.IOnConnectionFailedListener
    {
        private static GoogleSignInClient _googleSignInClient;
        private static GoogleManager _instance;
        private static Action<GoogleUser, string> _onLoginComplete;

        public static GoogleManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GoogleManager();
                }

                return _instance;
            }
        }

        public void Login(Action<GoogleUser, string> onLoginComplete)
        {
            _onLoginComplete = onLoginComplete;

            if (_googleSignInClient == null)
            {
                GoogleSignInOptions gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
                    .RequestEmail()
                    .Build();

                _googleSignInClient = GoogleSignIn.GetClient(Android.App.Application.Context, gso);
            }

            var signInIntent = _googleSignInClient.SignInIntent;
            ((MainActivity)Forms.Context).StartActivityForResult(signInIntent, 1);
        }

        public void Logout()
        {
            _googleSignInClient?.SignOut();
        }

        public void OnConnectionFailed(ConnectionResult result)
        {
            _onLoginComplete?.Invoke(null, result.ErrorMessage);
        }

        //public static async void OnAuthCompleted(GoogleSignInAccount account)
        //{
        //    if (account != null)
        //    {
        //        var pictureUrl = account.PhotoUrl?.ToString();

        //        _onLoginComplete?.Invoke(new GoogleUser()
        //        {
        //            Name = account.DisplayName,
        //            Email = account.Email,
        //            //Picture = pictureUrl
        //        }, string.Empty);
        //    }
        //    else
        //    {
        //        _onLoginComplete?.Invoke(null, "An error occurred!");
        //    }
        //}

    }
}
