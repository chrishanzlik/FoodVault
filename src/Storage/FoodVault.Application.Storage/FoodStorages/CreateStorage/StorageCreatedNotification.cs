using FoodVault.Application.Events;
using FoodVault.Domain.Storage.FoodStorages;
using FoodVault.Domain.Storage.FoodStorages.Events;
using Newtonsoft.Json;

namespace FoodVault.Application.Storage.FoodStorages.CreateStorage
{
    public class StorageCreatedNotification : DomainEventNotification<FoodStorageCreatedEvent>
    {
        public StorageCreatedNotification(FoodStorageCreatedEvent domainEvent) : base(domainEvent)
        {
            FoodStorageId = domainEvent.FoodStorageId;
        }

        [JsonConstructor]
        public StorageCreatedNotification(FoodStorageId foodStorageId) : base(null)
        {
            FoodStorageId = foodStorageId;
        }

        public FoodStorageId FoodStorageId { get; }
    }
}
