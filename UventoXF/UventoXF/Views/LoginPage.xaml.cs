using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using UventoXF.ViewModels;

namespace UventoXF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginPageViewModel(Navigation);
        }

        public void PasswordDimenticata_Clicked(object sender, EventArgs e)
        {

        }
        public void GoogleLogin(object sender, EventArgs e)
        {
            App.Current.MainPage = new MainPage();
        }

    }
}