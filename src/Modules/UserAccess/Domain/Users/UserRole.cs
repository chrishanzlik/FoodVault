using FoodVault.Framework.Domain;

namespace FoodVault.Modules.UserAccess.Domain.Users
{
    /// <summary>
    /// User role value object.
    /// </summary>
    public class UserRole : ValueObject
    {
        /// <summary>
        /// Required by Entity Framework Core.
        /// </summary>
        private UserRole()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRole" /> class.
        /// </summary>
        /// <param name="value">Role string value.</param>
        public UserRole(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static UserRole Member => new UserRole(nameof(Member));
        public static UserRole Administrator => new UserRole(nameof(Administrator));
    }
}
