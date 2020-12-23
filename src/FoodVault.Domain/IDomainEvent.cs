using System;

namespace FoodVault.Domain
{
    /// <summary>
    /// Interface for most basic domain events.
    /// </summary>
    public interface IDomainEvent
    {
        /// <summary>
        /// Gets the date and time when the event occured at.
        /// </summary>
        DateTime RaisedAt { get; }
    }
}
