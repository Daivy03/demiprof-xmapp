using System;
using System.Threading.Tasks;
using UventoXF.ViewModel;
using UventoXF.Views;
using Xamarin.Forms;

namespace UventoXF.ViewModels
{
    public class StartPageViewModel : BaseViewModel
    {
        public StartPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            NavigateToMainPageCommand = new Command(async () => await ExecuteNavigateToLoginPageCommand());
        }

        public Command NavigateToMainPageCommand { get; }

        private async Task ExecuteNavigateToLoginPageCommand()
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}
