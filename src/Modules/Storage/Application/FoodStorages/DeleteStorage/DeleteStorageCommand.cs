using FoodVault.Framework.Application.Commands;
using System;

namespace FoodVault.Modules.Storage.Application.FoodStorages.DeleteStorage
{
    /// <summary>
    /// Command for deleting food storages.
    /// </summary>
    public class DeleteStorageCommand : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteStorageCommand" /> class.
        /// </summary>
        /// <param name="foodStorageId">Storage identifier.</param>
        public DeleteStorageCommand(Guid foodStorageId)
        {
            FoodStorageId = foodStorageId;
        }

        /// <summary>
        /// Gets the id of the storage which should be deleted.
        /// </summary>
        public Guid FoodStorageId { get; }
    }
}
