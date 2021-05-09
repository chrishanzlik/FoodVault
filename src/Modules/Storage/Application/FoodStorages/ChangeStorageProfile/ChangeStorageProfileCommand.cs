using FoodVault.Framework.Application.Commands;
using System;

namespace FoodVault.Modules.Storage.Application.FoodStorages.ChangeStorageProfile
{
    /// <summary>
    /// Change profile data of a food storage.
    /// </summary>
    public class ChangeStorageProfileCommand : Command
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeStorageProfileCommand" /> class.
        /// </summary>
        /// <param name="foodStorageId">Food storage identifier.</param>
        /// <param name="storageName">New food storage name.</param>
        /// <param name="storageDescription">New food storage description.</param>
        public ChangeStorageProfileCommand(
            Guid foodStorageId,
            string storageName,
            string storageDescription)
        {
            StorageName = storageName;
            StorageDescription = storageDescription;
            FoodStorageId = foodStorageId;
        }

        /// <summary>
        /// Gets the food storage identifier.
        /// </summary>
        public Guid FoodStorageId { get; }

        /// <summary>
        /// Gets the new food storage name.
        /// </summary>
        public string StorageName { get; }

        /// <summary>
        /// Gets the new food storage description.
        /// </summary>
        public string StorageDescription { get; }
    }
}
