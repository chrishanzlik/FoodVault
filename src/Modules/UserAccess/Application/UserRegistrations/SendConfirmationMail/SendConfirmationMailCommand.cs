using FoodVault.Framework.Application.Commands;
using FoodVault.Modules.UserAccess.Domain;
using FoodVault.Modules.UserAccess.Domain.UserRegistrations;
using Newtonsoft.Json;
using System;

namespace FoodVault.Modules.UserAccess.Application.UserRegistrations.SendConfirmationMail
{
    /// <summary>
    /// Internal command for sending user confirmation mails.
    /// </summary>
    public class SendConfirmationMailCommand : InternalCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SendConfirmationMailCommand" /> class.
        /// </summary>
        /// <param name="id">Commands identifier.</param>
        /// <param name="userRegistrationId">Registrations identifier.</param>
        /// <param name="email">Users email.</param>
        [JsonConstructor]
        public SendConfirmationMailCommand(
            Guid id,
            UserRegistrationId userRegistrationId,
            EmailAddress email)
            : base(id)
        {
            UserRegistrationId = userRegistrationId;
            Email = email;
        }

        /// <summary>
        /// Gets the user registration identifer.
        /// </summary>
        internal UserRegistrationId UserRegistrationId { get; }

        /// <summary>
        /// Gets the users mail.
        /// </summary>
        internal EmailAddress Email { get; }
    }
}
