using System;

namespace FoodVault.Framework.Application.Outbox
{
    /// <summary>
    /// Outbox message.
    /// </summary>
    public class OutboxMessage
    {
        /// <summary>
        /// Required by Entity Framework.
        /// </summary>
        private OutboxMessage()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OutboxMessage" /> class.
        /// </summary>
        /// <param name="raisingTime"></param>
        /// <param name="eventType"></param>
        /// <param name="payload"></param>
        public OutboxMessage(Guid id, DateTime raisingTime, string eventType, string payload)
        {
            Id = id;
            RaisingTime = raisingTime;
            EventType = eventType;
            Payload = payload;
        }

        /// <summary>
        /// Gets the identifier of the message.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets the date and time when the event was raised.
        /// </summary>
        public DateTime RaisingTime { get; set; }

        /// <summary>
        /// Gets the type of the raised event.
        /// </summary>
        public string EventType { get; set; }

        /// <summary>
        /// Gets the event data.
        /// </summary>
        public string Payload { get; set; }

        /// <summary>
        /// Gets the date when the outbox message was processed.
        /// 'Null' for unprocessed / pending messages.
        /// </summary>
        public DateTime? ProcessedDate { get; set; }
    }
}
