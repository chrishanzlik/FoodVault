using FoodVault.Framework.Domain;

namespace FoodVault.Modules.UserAccess.Domain.Users.Events
{
    /// <summary>
    /// Domain event which signals that a user was created.
    /// </summary>
    public class UserCreatedEvent : DomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserCreatedEvent" /> class.
        /// </summary>
        /// <param name="userId">Id of the created user.</param>
        public UserCreatedEvent(UserId userId)
        {
            UserId = userId;
        }

        /// <summary>
        /// Gets the id of the user.
        /// </summary>
        public UserId UserId { get; }
    }
}
