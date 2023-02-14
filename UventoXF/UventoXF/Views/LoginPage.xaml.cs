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


namespace UventoXF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private readonly IGoogleManager _googleManager;
        GoogleUser GoogleUser = new GoogleUser();
        


        public bool IsLogedIn { get; set; }

        public LoginPage()
        {
            InitializeComponent();
            _googleManager = DependencyService.Get<IGoogleManager>();
            CheckUserLoggedIn();
         //   BindingContext = new LoginPageViewModel(Navigation);

            
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
                DisplayAlert("Message", message, "Ok");
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
        
        public void LoginAuth(object sender, EventArgs e)
        {
            App.Current.MainPage = new MainPage();
        }

    }
}