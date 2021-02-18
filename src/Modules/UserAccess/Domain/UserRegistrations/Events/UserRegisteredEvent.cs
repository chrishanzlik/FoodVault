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
        public UserRegisteredEvent(UserRegistrationId registrationId)
        {
            UserRegistrationId = registrationId;
        }

        /// <summary>
        /// Gets the identifier of the registration.
        /// </summary>
        public UserRegistrationId UserRegistrationId { get; }
    }
}
