using FoodVault.Framework.Application.Commands;
using System;

namespace FoodVault.Modules.UserAccess.Application.UserRegistrations.ConfirmRegistration
{
    /// <summary>
    /// Confirms a user registration.
    /// </summary>
    public class ConfirmUserRegistrationCommand : Command
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfirmUserRegistrationCommand" /> class.
        /// </summary>
        /// <param name="userRegistrationId">Users registration identifier.</param>
        public ConfirmUserRegistrationCommand(Guid userRegistrationId)
        {
            UserRegistrationId = userRegistrationId;
        }

        public Guid UserRegistrationId { get; }
    }
}
