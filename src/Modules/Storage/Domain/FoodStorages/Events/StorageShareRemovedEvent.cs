using FoodVault.Framework.Domain;
using FoodVault.Modules.Storage.Domain.Users;

namespace FoodVault.Modules.Storage.Domain.FoodStorages.Events
{
    /// <summary>
    /// Domain event which signals that a storage share was removed.
    /// </summary>
    public class StorageShareRemovedEvent : DomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StorageShareRemovedEvent" /> class.
        /// </summary>
        /// <param name="storageId">Id of the share.</param>
        /// <param name="userId">Id of the user.</param>
        public StorageShareRemovedEvent(UserId userId, FoodStorageId storageId)
        {
            FoodStorageId = storageId;
            UserId = userId;
        }

        /// <summary>
        /// Gets the id of the storage.
        /// </summary>
        public FoodStorageId FoodStorageId { get; }

        /// <summary>
        /// Gets the id of the user.
        /// </summary>
        public UserId UserId { get; }
    }
}
