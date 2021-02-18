using FoodVault.Framework.Domain;

namespace FoodVault.Modules.UserAccess.Domain.UserRegistrations.Events
{
    /// <summary>
    /// Domain event which signals that a registration was expired.
    /// </summary>
    public class UserRegistrationExpiredEvent : DomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRegisteredEvent" /> class.
        /// </summary>
        /// <param name="registrationId">Id of the registration.</param>
        public UserRegistrationExpiredEvent(UserRegistrationId registrationId)
        {
            UserRegistrationId = registrationId;
        }

        /// <summary>
        /// Gets the identifier of the registration.
        /// </summary>
        public UserRegistrationId UserRegistrationId { get; }
    }
}
