using FoodVault.Framework.Application.Commands;

namespace FoodVault.Modules.UserAccess.Application.Authentication.Authenticate
{
    /// <summary>
    /// Authenticates an user against the system.
    /// </summary>
    public class AuthenticateCommand : Command
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticateCommand" /> class.
        /// </summary>
        /// <param name="email">Email address of the authentication request.</param>
        /// <param name="password">Password of the authentication request.</param>
        public AuthenticateCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        /// <summary>
        /// Gets the email of the authentication request.
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// Gets the password of the authentication request.
        /// </summary>
        public string Password { get; }
    }
}
