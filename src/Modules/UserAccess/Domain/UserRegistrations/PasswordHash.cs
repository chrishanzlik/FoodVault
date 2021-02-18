using FoodVault.Framework.Domain;

namespace FoodVault.Modules.UserAccess.Domain.UserRegistrations
{
    public class PasswordHash : ValueObject
    {
        public PasswordHash(string hash)
        {
            Value = hash;
        }

        public string Value { get; }
    }
}
