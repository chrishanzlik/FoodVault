using FoodVault.Framework.Application.Commands;
using System;

namespace FoodVault.Modules.Storage.Application.FoodStorages.UnshareStorage
{
    /// <summary>
    /// Command for unsharing a storage for a user.
    /// </summary>
    public class UnshareStorageCommand : Command
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnshareStorageCommand" /> class.
        /// </summary>
        /// <param name="foodStorageId">Idientifer of the storage.</param>
        /// <param name="userId">Identifier of the user where the share should be removed.</param>
        public UnshareStorageCommand(Guid foodStorageId, Guid userId)
        {
            FoodStorageId = foodStorageId;
            UserId = userId;
        }

        /// <summary>
        /// Gets the food storage identifier.
        /// </summary>
        public Guid FoodStorageId { get; }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        public Guid UserId { get; }
    }
}
