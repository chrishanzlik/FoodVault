namespace FoodVault.Domain.Storage.FoodStore.Events
{
    /// <summary>
    /// Domain event which signals that a store was created.
    /// </summary>
    public class FoodStoreCreatedEvent : DomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FoodStoreCreatedEvent" /> class.
        /// </summary>
        /// <param name="storeId">Id of the created store.</param>
        public FoodStoreCreatedEvent(FoodStoreId storeId)
        {
            FoodStoreId = storeId;
        }

        /// <summary>
        /// Gets the id of the store.
        /// </summary>
        public FoodStoreId FoodStoreId { get; }
    }
}
