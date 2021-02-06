using System;

namespace FoodVault.Modules.Storage.Infrastructure.Configuration.Processing.Inbox
{
    /// <summary>
    /// Inbox message data transfer object.
    /// </summary>
    public class InboxMessageDto
    {
        /// <summary>
        /// Gets the identifier of the inbox message.
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
