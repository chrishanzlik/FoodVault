using System;

namespace FoodVault.Framework.Infrastructure.DomainEvents
{
    /// <summary>
    /// Maps notification types and names together.
    /// </summary>
    public interface IDomainNotificationsRegistry
    {
        /// <summary>
        /// Gets the notification name by a given type.
        /// </summary>
        /// <param name="type">Type of the notification.</param>
        /// <returns>Name of the notification.</returns>
        string GetName(Type type);

        /// <summary>
        /// Gets the notification type by a given name.
        /// </summary>
        /// <param name="name">Name of the notificaiton.</param>
        /// <returns>Type of the notification.</returns>
        Type GetType(string name);
    }
}
