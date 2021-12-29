using System;
using System.Security.Cryptography;
using System.Text;

namespace FoodVault.Modules.UserAccess.Application.Authentication
{
    public class PasswordManager : IPasswordManager
    {
        public string HashPassword(string password)
        {
            //TODO: impl with salt
            var sha = SHA512.Create();
            return Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }
    }
}
