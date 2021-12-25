using FoodVault.Framework.Domain;
using FoodVault.Modules.Storage.Domain.Users;

namespace FoodVault.Modules.Storage.Domain.FoodStorages.Events
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
        /// <param name="storageId">Id of the executing user.</param>
        public FoodStorageDeletedEvent(FoodStorageId storageId, UserId userId)
        {
            FoodStorageId = storageId;
            UserId = userId;
        }

        /// <summary>
        /// Gets the id of the storage.
        /// </summary>
        public FoodStorageId FoodStorageId { get; }

        /// <summary>
        /// Gets the id of the executing user.
        /// </summary>
        public UserId UserId { get; }
    }
}
