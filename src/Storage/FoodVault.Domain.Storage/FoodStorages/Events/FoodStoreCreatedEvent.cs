namespace FoodVault.Domain.Storage.FoodStorages.Events
{
    /// <summary>
    /// Domain event which signals that a storage was created.
    /// </summary>
    public class FoodStoreCreatedEvent : DomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FoodStoreCreatedEvent" /> class.
        /// </summary>
        /// <param name="storageId">Id of the created store.</param>
        public FoodStoreCreatedEvent(FoodStorageId storageId)
        {
            FoodStorageId = storageId;
        }

        /// <summary>
        /// Gets the id of the storage.
        /// </summary>
        public FoodStorageId FoodStorageId { get; }
    }
}
