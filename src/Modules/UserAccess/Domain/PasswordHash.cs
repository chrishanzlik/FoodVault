using FoodVault.Framework.Domain;

namespace FoodVault.Modules.UserAccess.Domain
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
