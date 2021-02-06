using FoodVault.Modules.Storage.Domain.FoodStorages.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Application.FoodStorages.CreateStorage
{
    internal class StorageCreatedDomainEventHandler : INotificationHandler<FoodStorageCreatedEvent>
    {
        public StorageCreatedDomainEventHandler()
        {

        }

        public Task Handle(FoodStorageCreatedEvent notification, CancellationToken cancellationToken)
        {
            // Apply changes on other aggregates etc.

            return Task.CompletedTask;
        }
    }
}
