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

        }
        public Command NavigateToMainPageCommand { get; }

        private async Task ExecuteNavigateToMainPageCommand()
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}