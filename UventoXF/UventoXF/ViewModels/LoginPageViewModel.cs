using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UventoXF.ViewModel;
using UventoXF.Views;
using Xamarin.Forms;

namespace UventoXF.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
//        private Button btn;

        public LoginPageViewModel(INavigation navigation)

        {
            this.Title = "Accedi";
            
            Navigation = navigation;
            NavigateToMainPageCommand = new Command(async () => await ExecuteNavigateToMainPageCommand());
            NavigateToRegisterPageCommand = new Command(async () => await ExecuteNavigateToRegisterPageCommand());

        }
        public Command NavigateToMainPageCommand { get; }
        public Command NavigateToRegisterPageCommand { get; }

        private async Task ExecuteNavigateToMainPageCommand()
        {
            await Navigation.PushAsync(new MainPage());
        }
        
        private async Task ExecuteNavigateToRegisterPageCommand()
        {
            await Navigation.PushAsync(new RegisterPage());
        }

        public void PasswordDimenticata_Clicked(object sender, EventArgs e)
        {

        }
        public void GoogleLogin(object sender, EventArgs e)
        {
        //    App.Current.MainPage = new MainPage();
        Navigation.PushAsync(new RegisterPage());
        }
    }
}