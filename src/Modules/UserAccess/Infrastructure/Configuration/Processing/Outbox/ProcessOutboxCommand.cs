using FoodVault.Framework.Application.Commands;

namespace FoodVault.Modules.UserAccess.Infrastructure.Configuration.Processing.Outbox
{
    /// <summary>
    /// Command for triggering the outbox processing.
    /// </summary>
    internal class ProcessOutboxCommand : Command, IRecurringCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessOutboxCommand" /> class.
        /// </summary>
        public ProcessOutboxCommand()
        {
        }
    }
}
