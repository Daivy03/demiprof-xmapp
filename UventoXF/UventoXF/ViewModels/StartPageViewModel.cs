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
            NavigateToLoginPageCommand = new Command(async () => await ExecuteNavigateToLoginPageCommand());
        }

        public Command NavigateToLoginPageCommand { get; }

        private async Task ExecuteNavigateToLoginPageCommand()
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}
