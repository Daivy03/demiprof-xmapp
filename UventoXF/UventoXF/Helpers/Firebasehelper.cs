﻿using Firebase.Database;
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
            Id = u.Object.Id,
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

    public static async Task<User> GetUserById(string id)
    {
        var firebase = new FirebaseClient(FirebaseConfig.FirebaseUrl);
        var users = await firebase.Child("users").OnceAsync<User>();
        return users.Where(u => (string)u.Object.Id == id).Select(u => u.Object).FirstOrDefault();
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
