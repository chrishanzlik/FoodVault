using System;

namespace FoodVault.Framework.Infrastructure.Outbox
{
    /// <summary>
    /// Outbot message data transfer object.
    /// </summary>
    public class OutboxMessageDto
    {
        /// <summary>
        /// Gets the identifier of the <see cref="OutboxMessage"/>.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets the event type.
        /// </summary>
        public string EventType { get; set; }

        /// <summary>
        /// Gets the event payload.
        /// </summary>
        public string Payload { get; set; }
    }
}
