namespace FoodVault.Api.Modules.UserAccess.UserRegistrations
{
    /// <summary>
    /// Request data for registrate a new user.
    /// </summary>
    public class RegisterUserRequest
    {
        /// <summary>
        /// Gets or sets the users email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the users blank password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the users first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the users last name.
        /// </summary>
        public string LastName { get; set; }
    }
}