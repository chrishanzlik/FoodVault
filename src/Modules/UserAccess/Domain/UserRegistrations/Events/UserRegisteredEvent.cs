using FoodVault.Framework.Domain;

namespace FoodVault.Modules.UserAccess.Domain.UserRegistrations.Events
{
    /// <summary>
    /// Domain event which signals that a user was registered.
    /// </summary>
    public class UserRegisteredEvent : DomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRegisteredEvent" /> class.
        /// </summary>
        /// <param name="registrationId">Id of the registration.</param>
        /// <param name="email">Users email address.</param>
        public UserRegisteredEvent(UserRegistrationId registrationId, EmailAddress email)
        {
            UserRegistrationId = registrationId;
            Email = email;
        }

        /// <summary>
        /// Gets the identifier of the registration.
        /// </summary>
        public UserRegistrationId UserRegistrationId { get; }

        /// <summary>
        /// Gets the users email address.
        /// </summary>
        public EmailAddress Email { get; set; }
    }
}
