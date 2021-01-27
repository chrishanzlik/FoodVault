using System;

namespace FoodVault.Domain
{
    /// <summary>
    /// Base class that defines a domain event.
    /// </summary>
    public abstract class DomainEvent : IDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEvent" /> class.
        /// </summary>
        public DomainEvent()
        {
            RaisedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets the point when the event was raised.
        /// </summary>
        public DateTime RaisedAt { get; }
    }
}
