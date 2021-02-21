using FoodVault.Framework.Domain;

namespace FoodVault.Modules.UserAccess.Domain
{
    public class PasswordHash : ValueObject
    {
        public PasswordHash(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}
