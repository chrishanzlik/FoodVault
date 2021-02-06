using MediatR;
using System;

namespace FoodVault.Framework.Infrastructure.EventBus
{
    /// <summary>
    /// Represents a cross boundary integration event.
    /// </summary>
    public abstract class IntegrationEvent : INotification
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IntegrationEvent" /> class.
        /// </summary>
        /// <param name="id">Identifier of the integration event.</param>
        /// <param name="occurredOn">When the integration event occurs.</param>
        protected IntegrationEvent(Guid id, DateTime occurredOn)
        {
            Id = id;
            OccurredOn = occurredOn;
        }

        /// <summary>
        /// Gets the identifier of the integration event.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Gets the time, at which the event occured on.
        /// </summary>
        public DateTime OccurredOn { get; }
    }
}
