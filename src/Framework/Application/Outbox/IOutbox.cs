using System.Threading.Tasks;

namespace FoodVault.Framework.Application.Outbox
{
    /// <summary>
    /// Stores informations that should processed outside of a transaction.
    /// </summary>
    public interface IOutbox
    {
        /// <summary>
        /// Adds a <see cref="OutboxMessage"/> to the outbox.
        /// </summary>
        /// <param name="message">Message to add.</param>
        void Add(OutboxMessage message);

        /// <summary>
        /// Persists all added <see cref="OutboxMessage"/>s to the outbox.
        /// </summary>
        /// <returns>Awaitable task.</returns>
        Task SaveAsync();
    }
}
