using FoodVault.Modules.Storage.Domain.FoodStorages;
using FoodVault.Modules.Storage.Domain.FoodStorages.Events;
using FoodVault.Framework.Application.Events;
using Newtonsoft.Json;

namespace FoodVault.Modules.Storage.Application.FoodStorages.CreateStorage
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
