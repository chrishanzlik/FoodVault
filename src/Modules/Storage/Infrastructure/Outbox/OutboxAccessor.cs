using FoodVault.Framework.Application.Outbox;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Infrastructure.Outbox
{
    internal class OutboxAccessor : IOutbox
    {
        private StorageContext _storageContext;

        public OutboxAccessor(StorageContext storageContext)
        {
            _storageContext = storageContext;
        }

        public void Add(OutboxMessage message)
        {
            _storageContext.OutboxMessages.Add(message);
        }

        public Task SaveAsync()
        {
            return Task.CompletedTask;
        }
    }
}
