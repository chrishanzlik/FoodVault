using FoodVault.Framework.Domain;

namespace FoodVault.Modules.Storage.Domain.FoodStorages.Events
{
    /// <summary>
    /// Domain event which signals that a storage was created.
    /// </summary>
    public class FoodStorageCreatedEvent : DomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FoodStorageCreatedEvent" /> class.
        /// </summary>
        /// <param name="storageId">Id of the created storage.</param>
        public FoodStorageCreatedEvent(FoodStorageId storageId)
        {
            FoodStorageId = storageId;
        }

        /// <summary>
        /// Gets the id of the storage.
        /// </summary>
        public FoodStorageId FoodStorageId { get; }
    }
}
