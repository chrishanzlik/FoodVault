using FoodVault.Framework.Domain;

namespace FoodVault.Modules.UserAccess.Domain.Users
{
    /// <summary>
    /// User role value object.
    /// </summary>
    public class UserRole : ValueObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRole" /> class.
        /// </summary>
        /// <param name="roleValue">Role string value.</param>
        public UserRole(string roleValue)
        {
            RoleValue = roleValue;
        }

        public string RoleValue { get; }

        public static UserRole Member => new UserRole(nameof(Member));
        public static UserRole Administrator => new UserRole(nameof(Administrator));
    }
}
