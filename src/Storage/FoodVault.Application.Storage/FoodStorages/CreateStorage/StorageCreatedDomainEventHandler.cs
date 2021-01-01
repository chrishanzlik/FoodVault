using FoodVault.Domain.Storage.FoodStorages.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Application.Storage.FoodStorages.CreateStorage
{
    public class StorageCreatedDomainEventHandler : INotificationHandler<FoodStorageCreatedEvent>
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
