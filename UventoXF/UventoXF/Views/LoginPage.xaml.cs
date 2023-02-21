using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using UventoXF.ViewModels;
using DemiProf.Models;
using UventoXF.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UventoXF.Views;
using UventoXF.Helpers;
using Plugin.Toast;
using UventoXF.Interfaces;


namespace UventoXF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private readonly IGoogleManager _googleManager;
        GoogleUser GoogleUser = new GoogleUser();
        FirebaseHelper FirebaseHelper = new FirebaseHelper();



        public bool IsLogedIn { get; set; }



        private async void Register_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }

        public LoginPage()
        {
            InitializeComponent();
            _googleManager = DependencyService.Get<IGoogleManager>();
            CheckUserLoggedIn();
            BindingContext = new LoginPageViewModel(Navigation);


        }
        private void OnShowPasswordCheckboxCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                EntryPassword.IsPassword = false;
            }
            else
            {
                EntryPassword.IsPassword = true;
            }
        }

        private void CheckUserLoggedIn()
        {
            _googleManager.Login(OnLoginComplete);
        }
        private void btnLogin_Clicked(object sender, EventArgs e)
        {
            _googleManager.Login(OnLoginComplete);
        }
        private void OnLoginComplete(GoogleUser googleUser, string message)
        {

            try
            {
                var AvatarIMG = (Image)FindByName("AvatarIMG");
                if (googleUser != null)
                {
                    GoogleUser = googleUser;
                    EntryEmail.Text = GoogleUser.Name;
                    EntryPassword.Text = GoogleUser.Email;
                    AvatarIMG.Source = GoogleUser.Picture;
                    IsLogedIn = true;
                }
                else
                {
                    DisplayAlert("Login non riuscito!", message, "Ok");
                }
            }
            catch
            {

            }

        }
        private void GoogleLogout()
        {
            _googleManager.Logout();
            IsLogedIn = false;
        }
        private void btnLogout_Clicked(object sender, EventArgs e)
        {
            var AvatarIMG = (Image)FindByName("AvatarIMG");
            _googleManager.Logout();

            EntryEmail.Text = "Name :";
            EntryPassword.Text = "Email: ";
            AvatarIMG.Source = "";
        }

        public void PasswordDimenticata_Clicked(object sender, EventArgs e)
        {

        }
        public void GoogleLogin(object sender, EventArgs e)
        {
            App.Current.MainPage = new MainPage();
        }
        

        public async void LoginAuth(object sender, EventArgs e)
        {
            //TODO: Crash app durante la visualizzazione del popup toast

            // Recupera l'indirizzo email e la password inseriti dall'utente
            string email = EntryEmail?.Text;
            string password = EntryPassword?.Text;

            

            // Verifica se l'utente esiste nel database Firebase
            bool userExists = await FirebaseHelper.CheckIfUserExists(email);

            if (userExists)
            {
                // Recupera l'utente dal database Firebase
                User user = await FirebaseHelper.GetUserByEmail(email);

                // Verifica se la password inserita corrisponde alla password memorizzata nel database
                if (user?.Password == password)
                {
                    // La password è corretta, esegui il login
                    
                    //if(user.Id != null)
                    //{
                    //    App.Current.Properties["UserId"] = user.Id;
                    //    await App.Current.SavePropertiesAsync();
                    //}
                    //else
                    //{
                        
                    //}
                    //DependencyService.Get<IToast>().Show("Accesso eseguito correttamente!");
                    App.Current.MainPage = new MainPage();
                }
                else
                {
                    // La password non corrisponde, mostra un messaggio di errore
                    //DependencyService.Get<IToast>().Show("La password inserita non è corretta.");
                }
            }
            else
            {
                // L'utente non esiste, mostra un messaggio di errore
                //DependencyService.Get<IToast>().Show("L'indirizzo email inserito non è associato ad alcun account.");
            }
        }


    }
}