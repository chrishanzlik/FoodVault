using FoodVault.Framework.Domain;
using Newtonsoft.Json;
using System;

namespace FoodVault.Framework.Application.Events
{
    /// <summary>
    /// Notification about an occured domain event.
    /// </summary>
    /// <typeparam name="TEvent">Type of the domain event.</typeparam>
    public class DomainEventNotification<TEvent> : IDomainEventNotification<TEvent>
        where TEvent : IDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEventNotification" /> class.
        /// </summary>
        /// <param name="domainEvent">Domain event to notify about.</param>
        public DomainEventNotification(TEvent domainEvent)
        {
            Id = Guid.NewGuid();
            DomainEvent = domainEvent;
        }

        /// <inheritdoc />
        [JsonIgnore]
        public TEvent DomainEvent { get; }

        /// <inheritdoc />
        public Guid Id { get; }
    }
}
