using FoodVault.Framework.Domain;
using FoodVault.Modules.Storage.Domain.Users;

namespace FoodVault.Modules.Storage.Domain.FoodStorages.Events
{
    /// <summary>
    /// Domain event which signals that a storage share was created.
    /// </summary>
    public class StorageShareCreatedEvent : DomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StorageShareCreatedEvent" /> class.
        /// </summary>
        /// <param name="storageShareId">Id of the share.</param>
        /// <param name="userId">Id of the user.</param>
        /// <param name="canWrite">If the user has write permission.</param>
        public StorageShareCreatedEvent(UserId userId, FoodStorageId storageId, bool canWrite)
        {
            FoodStorageId = storageId;
            UserId = userId;
            CanWrite = canWrite;
        }

        /// <summary>
        /// Gets the id of the storage.
        /// </summary>
        public FoodStorageId FoodStorageId { get; }

        /// <summary>
        /// Gets the id of the user.
        /// </summary>
        public UserId UserId { get; }

        /// <summary>
        /// Gets a value indicanting whether the user has write access.
        /// </summary>
        public bool CanWrite { get; set; }
    }
}
