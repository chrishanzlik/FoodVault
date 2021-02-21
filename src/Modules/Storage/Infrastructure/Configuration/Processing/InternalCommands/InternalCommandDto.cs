using System;

namespace FoodVault.Modules.Storage.Infrastructure.Configuration.Processing.InternalCommands
{
    /// <summary>
    /// Internal command data transfer object.
    /// </summary>
    public class InternalCommandDto
    {
        /// <summary>
        /// Gets or sets the command identifier.
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
    }
}
