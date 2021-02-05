using FoodVault.Framework.Application.Commands;

namespace FoodVault.Modules.Storage.Infrastructure.Configuration.Processing.Outbox
{
    /// <summary>
    /// Command for triggering the outbox processing.
    /// </summary>
    public class ProcessOutboxCommand : Command
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OutboxMessage" /> class.
        /// </summary>
        public ProcessOutboxCommand()
        {
        }
    }
}
