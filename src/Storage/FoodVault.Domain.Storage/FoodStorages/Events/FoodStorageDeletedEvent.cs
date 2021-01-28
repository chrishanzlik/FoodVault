namespace FoodVault.Domain.Storage.FoodStorages.Events
{
    /// <summary>
    /// Domain event which signals that a storage was deleted.
    /// </summary>
    public class FoodStorageDeletedEvent : DomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FoodStorageCreatedEvent" /> class.
        /// </summary>
        /// <param name="storageId">Id of the created storage.</param>
        public FoodStorageDeletedEvent(FoodStorageId storageId)
        {
            FoodStorageId = storageId;
        }

        /// <summary>
        /// Gets the id of the storage.
        /// </summary>
        public FoodStorageId FoodStorageId { get; }
    }
}
