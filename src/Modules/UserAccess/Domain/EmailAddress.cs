using FoodVault.Framework.Domain;

namespace FoodVault.Modules.UserAccess.Domain
{
    public class EmailAddress : ValueObject
    {
        public EmailAddress(string email)
        {
            Value = email;
        }

        public string Value { get; }
    }
}
