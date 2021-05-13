using FoodVault.Framework.Domain;
using FoodVault.Modules.Storage.Domain.FoodStorages.Events;
using FoodVault.Modules.Storage.Domain.FoodStorages.Rules;
using FoodVault.Modules.Storage.Domain.Users;
using System;

namespace FoodVault.Modules.Storage.Domain.FoodStorages
{
    public class StorageShare : Entity
    {
        private FoodStorageId _foodStorageId;
        private bool _canWrite;

        /// <summary>
        /// Required for Entity Framework Core.
        /// </summary>
        private StorageShare()
        {
        }

        private StorageShare(FoodStorageId storageId, UserId userId, bool canWrite)
        {
            UserId = userId;
            SharedAt = DateTime.UtcNow;

            _foodStorageId = storageId;
            _canWrite = canWrite;
        }

        /// <summary>
        /// Gets a value indicating whether the user has write access.
        /// </summary>
        internal bool IsWritePermissionEnabled => _canWrite;

        /// <summary>
        /// Gets the identifier of the user.
        /// </summary>
        public UserId UserId { get; }

        /// <summary>
        /// Gets the date when the share was created.
        /// </summary>
        public DateTime SharedAt { get; }

        /// <summary>
        /// Creates a share for a specific user.
        /// </summary>
        /// <param name="storageId"></param>
        /// <param name="userId"></param>
        /// <param name="canWrite"></param>
        /// <returns></returns>
        internal static StorageShare CreateForUser(FoodStorageId storageId, UserId userId, bool canWrite)
        {
            return new StorageShare(storageId, userId, canWrite);
        }

        /// <summary>
        /// Grants write permission.
        /// </summary>
        public void GrantWritePermission()
        {
            this.CheckDomainRule(new WritePermissionCanOnlyGrantedWhenNotAssignedRule(_canWrite));

            _canWrite = true;

            this.AddDomainEvent(new WritePermissionGrantedEvent(UserId, _foodStorageId));
        }

        /// <summary>
        /// Revokes the write permission.
        /// </summary>
        public void RevokeWritePermission()
        {
            this.CheckDomainRule(new WritePermissionCanOnlyRevokedWhenAssignedRule(_canWrite));

            _canWrite = false;

            this.AddDomainEvent(new WritePermissionRevokedEvent(UserId, _foodStorageId));
        }
    }
}
