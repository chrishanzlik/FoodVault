using FoodVault.Framework.Domain;

namespace FoodVault.Modules.UserAccess.Domain.Users
{
    public class EmailAddress : ValueObject
    {
        public EmailAddress(string email)
        {
            EmailValue = email;
        }

        public string EmailValue { get; }
    }
}
