using FoodVault.Framework.Domain;
using System.Collections.Generic;

namespace FoodVault.Framework.Infrastructure.DomainEvents
{
    /// <summary>
    /// Interface for accessing domain events.
    /// </summary>
    public interface IDomainEventAccessor
    {
        /// <summary>
        /// Gets all pending domain events.
        /// </summary>
        /// <returns>A readonly collection of pending domain events.</returns>
        IReadOnlyCollection<IDomainEvent> GetAllDomainEvents();

        /// <summary>
        /// Clears all pending domain events.
        /// </summary>
        void ClearAllDomainEvents();
    }
}
