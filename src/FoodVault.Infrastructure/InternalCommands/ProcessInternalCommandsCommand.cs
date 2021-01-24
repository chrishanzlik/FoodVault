using FoodVault.Application.Commands;

namespace FoodVault.Infrastructure.InternalCommands
{
    /// <summary>
    /// Command that triggers internal command processing.
    /// </summary>
    public class ProcessInternalCommandsCommand : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessInternalCommandsCommand" /> class.
        /// </summary>
        public ProcessInternalCommandsCommand()
        {

        }
    }
}
