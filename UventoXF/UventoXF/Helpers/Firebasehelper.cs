using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UventoXF.Helpers;
using UventoXF.Models;

public class FirebaseHelper
{
    FirebaseClient firebase;

    public FirebaseHelper()
    {
        firebase = new FirebaseClient(FirebaseConfig.FirebaseUrl);
    }

    public async Task<List<User>> GetAllUsers()
    {
        var users = await firebase
            .Child("users")
            .OnceAsync<User>();

        return users.Select(u => new User
        {
            UserId = u.Object.UserId,
            Email = u.Object.Email,
            FirstName = u.Object.FirstName,
            LastName = u.Object.LastName,
            Password = u.Object.Password
        }).ToList();
    }

    public async Task<User> GetUserByEmail(string email)
    {
        var user = (await firebase
            .Child("users")
            .OnceAsync<User>())
            .FirstOrDefault(a => a.Object.Email == email);

        if (user != null)
        {
            user.Object.UserId = user.Key;
            return user.Object;
        }

        return null;
    }



    public static async Task<User> GetUserById(string id)
    {
        var firebase = new FirebaseClient(FirebaseConfig.FirebaseUrl);
        var users = await firebase.Child("users").OnceAsync<User>();
        var user = users.Where(u => u.Object.UserId == id).Select(u => u.Object).FirstOrDefault();

        if (user != null)
        {
            user.FirstName = user.FirstName ?? string.Empty;
        }

        return user;
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
