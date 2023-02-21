using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using UventoXF.ViewModel;
using UventoXF.Views;
using Xamarin.Forms;

namespace UventoXF.ViewModels
{
    public class RegisterPageViewModel : BaseViewModel
    {
        public Command NavigateToLoginPageCommand { get; }

        public RegisterPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            NavigateToLoginPageCommand = new Command(async () => await ExecuteNavigateToLoginPageCommand());
        }

        private async Task ExecuteNavigateToLoginPageCommand()
        {
            await Navigation.PushAsync(new LoginPage());
        }

    }
}
