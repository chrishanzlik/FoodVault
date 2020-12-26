using FoodVault.Core.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodVault.Application.Storage.FoodStorages.CreateStorage
{
    /// <summary>
    /// Command for creating food storages.
    /// </summary>
    public class CreateStorageCommand : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateStorageCommand" /> class.
        /// </summary>
        /// <param name="storageName"></param>
        /// <param name="description"></param>
        public CreateStorageCommand(string storageName, string description)
        {
            StorageName = storageName;
            Description = description;
        }

        /// <summary>
        /// Gets the destinated name of the storage to create.
        /// </summary>
        public string StorageName { get; }

        /// <summary>
        /// Gets the description of the storage to create.
        /// </summary>
        public string Description { get; }
    }
}
