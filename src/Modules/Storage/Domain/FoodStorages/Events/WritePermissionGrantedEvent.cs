using FoodVault.Framework.Domain;
using FoodVault.Modules.Storage.Domain.Users;

namespace FoodVault.Modules.Storage.Domain.FoodStorages.Events
{
    /// <summary>
    /// Domain event which signals that a storage-share write permission was granted.
    /// </summary>
    public class WritePermissionGrantedEvent : DomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WritePermissionGrantedEvent" /> class.
        /// </summary>
        /// <param name="userId">Id of the user.</param>
        /// <param name="storageId">Id of the storage.</param>
        public WritePermissionGrantedEvent(UserId userId, FoodStorageId storageId)
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
