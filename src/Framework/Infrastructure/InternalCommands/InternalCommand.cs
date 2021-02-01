using System;

namespace FoodVault.Framework.Infrastructure.InternalCommands
{
    /// <summary>
    /// Internal triggered application command.
    /// </summary>
    public class InternalCommand
    {
        /// <summary>
        /// Gets the identifier of the internal command.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets the type of the command.
        /// </summary>
        public string CommandType { get; set; }

        /// <summary>
        /// Gets the commands data.
        /// </summary>
        public string Payload { get; set; }

        /// <summary>
        /// Gets the time when the command was processed.
        /// If null, the command is not processed yet.
        /// </summary>
        public DateTime? ProcessedDate { get; set; }
    }
}
