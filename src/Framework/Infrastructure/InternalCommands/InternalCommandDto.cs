namespace FoodVault.Framework.Infrastructure.InternalCommands
{
    /// <summary>
    /// Internal command data transfer object.
    /// </summary>
    public class InternalCommandDto
    {
        /// <summary>
        /// Gets the type of the command.
        /// </summary>
        public string CommandType { get; set; }

        /// <summary>
        /// Gets the commands data.
        /// </summary>
        public string Payload { get; set; }
    }
}
