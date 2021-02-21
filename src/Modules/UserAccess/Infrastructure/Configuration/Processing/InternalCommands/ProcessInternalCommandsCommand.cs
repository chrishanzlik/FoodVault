using FoodVault.Framework.Application.Commands;

namespace FoodVault.Modules.UserAccess.Infrastructure.Configuration.Processing.InternalCommands
{
    /// <summary>
    /// Command that triggers internal command processing.
    /// </summary>
    internal class ProcessInternalCommandsCommand : Command, IRecurringCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessInternalCommandsCommand" /> class.
        /// </summary>
        public ProcessInternalCommandsCommand()
        {
        }
    }
}
