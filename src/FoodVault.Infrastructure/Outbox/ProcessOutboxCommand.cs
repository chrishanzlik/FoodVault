using FoodVault.Application.Mediator;

namespace FoodVault.Infrastructure.Outbox
{
    /// <summary>
    /// Command for triggering the outbox processing.
    /// </summary>
    public class ProcessOutboxCommand : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OutboxMessage" /> class.
        /// </summary>
        public ProcessOutboxCommand()
        {

        }
    }
}
