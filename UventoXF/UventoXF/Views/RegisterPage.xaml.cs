using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UventoXF.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Firebase.Database;
using Firebase.Database.Query;
using UventoXF.Models;
using Rg.Plugins.Popup.Services;

namespace UventoXF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://demiprof-b12be-default-rtdb.europe-west1.firebasedatabase.app/");
        public RegisterPage()
        {
            InitializeComponent();
            BindingContext = new RegisterPageViewModel(Navigation);


        }

        private async Task SendUser()
        {
            var firstName = FirstNameEntry.Text;
            var lastName = LastNameEntry.Text;
            var email = EmailEntry.Text;
            var password = PasswordEntry.Text;
            var newUser = new User(firstName, lastName, email, password);

            await firebaseClient.Child("users").PostAsync(newUser);
            var successModal = new RegistrationSuccessModal();
            successModal.BindingContext = this;
            await PopupNavigation.Instance.PushAsync(successModal);//Not working

            //    RegistrationMessageLabel.Text = "Registrazione completata con successo!";
        }
        public void OnSignUpClicked(object sender, EventArgs e)
        {
            Task task = SendUser();
        }


    }
}