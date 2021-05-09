using FoodVault.Framework.Domain;

namespace FoodVault.Modules.UserAccess.Domain
{
    public class EmailAddress : ValueObject
    {
        public EmailAddress(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}
