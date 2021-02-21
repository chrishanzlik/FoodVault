using System;

namespace FoodVault.Framework.Infrastructure.InternalCommands
{
    /// <summary>
    /// Internal triggered application command.
    /// </summary>
    public class InternalCommand
    {
        /// <summary>
        /// Gets or sets the identifier of the internal command.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the type of the command.
        /// </summary>
        public string CommandType { get; set; }

        /// <summary>
        /// Gets or sets the commands data.
        /// </summary>
        public string Payload { get; set; }

        /// <summary>
        /// Gets or sets an occured error while executing the command.
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Gets or sets the time, when the command was enqueued.
        /// </summary>
        public DateTime? EnqueueDate { get; set; }

        /// <summary>
        /// Gets or sets the time when the command was processed.
        /// If null, the command is not processed yet.
        /// </summary>
        public DateTime? ProcessedDate { get; set; }
    }
}
