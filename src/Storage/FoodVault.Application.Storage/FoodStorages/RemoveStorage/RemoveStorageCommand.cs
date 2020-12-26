using FoodVault.Core.Mediator;
using System;

namespace FoodVault.Application.Storage.FoodStorages.RemoveStorage
{
    /// <summary>
    /// Command for removing food storages.
    /// </summary>
    public class RemoveStorageCommand : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveStorageCommand" /> class.
        /// </summary>
        /// <param name="foodStorageId">Storage identifier.</param>
        public RemoveStorageCommand(Guid foodStorageId)
        {
            FoodStorageId = foodStorageId;
        }

        /// <summary>
        /// Gets the id of the storage which should be removed.
        /// </summary>
        public Guid FoodStorageId { get; }
    }
}
