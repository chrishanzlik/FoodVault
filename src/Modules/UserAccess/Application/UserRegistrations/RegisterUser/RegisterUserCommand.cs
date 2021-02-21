using FoodVault.Framework.Application.Commands;

namespace FoodVault.Modules.UserAccess.Application.UserRegistrations.RegisterUser
{
    /// <summary>
    /// Creates a user registration.
    /// </summary>
    public class RegisterUserCommand : Command
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterUserCommand" /> class.
        /// </summary>
        /// <param name="email">Users email.</param>
        /// <param name="password">Users password.</param>
        /// <param name="firstName">Users first name.</param>
        /// <param name="lastName">Users last name.</param>
        public RegisterUserCommand(string email, string password, string firstName, string lastName)
        {
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
        }

        /// <summary>
        /// Gets the email address.
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// Gets the specified password.
        /// </summary>
        public string Password { get; }

        /// <summary>
        /// Gets the users first name.
        /// </summary>
        public string FirstName { get; }

        /// <summary>
        /// Gets the users last name.
        /// </summary>
        public string LastName { get; }
    }
}
