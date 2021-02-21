using FoodVault.Modules.Storage.Domain.FoodStorages;
using FoodVault.Modules.Storage.Domain.FoodStorages.Events;
using FoodVault.Framework.Application.Events;
using Newtonsoft.Json;
using System;

namespace FoodVault.Modules.Storage.Application.FoodStorages.CreateStorage
{
    /// <summary>
    /// Notification about an occured <see cref="FoodStorageCreatedEvent"/>.
    /// </summary>
    public class StorageCreatedNotification : DomainEventNotification<FoodStorageCreatedEvent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StorageCreatedNotification" /> class.
        /// </summary>
        /// <param name="domainEvent">Event to handle.</param>
        /// <param name="id">Notifications id.</param>
        public StorageCreatedNotification(FoodStorageCreatedEvent domainEvent, Guid id) : base(domainEvent, id)
        {
            FoodStorageId = domainEvent.FoodStorageId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageCreatedNotification" /> class.
        /// </summary>
        /// <param name="foodStorageId">Identifier of the food storage.</param>
        /// <param name="id">Notifications id.</param>
        [JsonConstructor]
        public StorageCreatedNotification(FoodStorageId foodStorageId, Guid id) : base(null, id)
        {
            FoodStorageId = foodStorageId;
        }

        /// <summary>
        /// Gets the identifier of the food storage.
        /// </summary>
        public FoodStorageId FoodStorageId { get; }
    }
}
