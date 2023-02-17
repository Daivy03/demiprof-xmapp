using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;

namespace UventoXF.ViewModels
{
    internal class RegisterPageViewModel
    {
        private FirebaseClient firebaseClient;

        public RegisterPageViewModel()
        {
            firebaseClient = new FirebaseClient("https://demiprof-b12be-default-rtdb.europe-west1.firebasedatabase.app");
        }

        public async Task RegisterUser(string username, string email, string password)
        {
            var user = new User
            {
                Username = username,
                Email = email,
                Password = password
            };

            // Path to the node where the user data will be stored
            var path = "users/" + username;

            // Push the user data to Firebase Realtime Database
          //  await firebaseClient.Child(path).PutAsync(user);
        }
    }

    public class User
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
