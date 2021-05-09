using FoodVault.Framework.Application.Commands;
using System;

namespace FoodVault.Modules.Storage.Application.FoodStorages.ShareStorage
{
    /// <summary>
    /// Command for sharing a storage with another user.
    /// </summary>
    public class ShareStorageCommand : Command
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShareStorageCommand" /> class.
        /// </summary>
        /// <param name="foodStorageId">Idientifer of the storage.</param>
        /// <param name="userId">Identifier of the user who is receiving the share.</param>
        /// <param name="writeAccess">Can the user perform write operations?</param>
        public ShareStorageCommand(Guid foodStorageId, Guid userId, bool writeAccess)
        {
            FoodStorageId = foodStorageId;
            UserId = userId;
            WriteAccess = writeAccess;
        }

        /// <summary>
        /// Gets the food storage identifier.
        /// </summary>
        public Guid FoodStorageId { get; }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// Gets a value indicating whether the user can perform write operations.
        /// </summary>
        public bool WriteAccess { get; }
    }
}
