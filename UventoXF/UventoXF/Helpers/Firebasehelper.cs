using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UventoXF.Models;

public class FirebaseHelper
{
    FirebaseClient firebase;

    public FirebaseHelper()
    {
        firebase = new FirebaseClient("https://demiprof-b12be-default-rtdb.europe-west1.firebasedatabase.app/");
    }

    public async Task<List<User>> GetAllUsers()
    {
        var users = await firebase
            .Child("users")
            .OnceAsync<User>();

        return users.Select(u => new User
        {
            Email = u.Object.Email,
            FirstName = u.Object.FirstName,
            LastName = u.Object.LastName,
            Password = u.Object.Password
        }).ToList();
    }

    public async Task<User> GetUserByEmail(string email)
    {
        var users = await GetAllUsers();
        return users.FirstOrDefault(u => u.Email == email);
    }
    public async Task<bool> CheckIfUserExists(string email)
    {
        var result = await firebase
            .Child("users")
            .OrderBy("Email")
            .EqualTo(email)
            .OnceAsync<User>();

        return result.Any();
    }
}
