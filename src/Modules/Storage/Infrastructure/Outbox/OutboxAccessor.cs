using FoodVault.Framework.Application.Outbox;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Infrastructure.Outbox
{
    /// <summary>
    /// Stores informations that should processed outside of a transaction.
    /// </summary>
    internal class OutboxAccessor : IOutbox
    {
        private StorageContext _storageContext;

        /// <inheritdoc />
        public OutboxAccessor(StorageContext storageContext)
        {
            _storageContext = storageContext;
        }

        /// <inheritdoc />
        public void Add(OutboxMessage message)
        {
            _storageContext.OutboxMessages.Add(message);
        }

        /// <inheritdoc />
        public Task SaveAsync()
        {
            return Task.CompletedTask;
        }
    }
}
