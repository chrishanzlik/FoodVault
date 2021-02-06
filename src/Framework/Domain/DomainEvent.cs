using System;

namespace FoodVault.Framework.Domain
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
            Id = Guid.NewGuid();
            RaisedAt = DateTime.UtcNow;
        }

        /// <inheritdoc />
        public DateTime RaisedAt { get; }

        /// <inheritdoc />
        public Guid Id { get; }
    }
}
