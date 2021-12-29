using FoodVault.Framework.Application.Commands;

namespace FoodVault.Modules.Storage.Application.FoodStorages.CreateStorage
{
    /// <summary>
    /// Command for creating food storages.
    /// </summary>
    public class CreateStorageCommand : Command
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateStorageCommand" /> class.
        /// </summary>
        /// <param name="storageName">Storage name.</param>
        /// <param name="description">Storage description.</param>
        public CreateStorageCommand(string storageName, string description)
        {
            StorageName = storageName?.Trim();
            Description = description?.Trim();
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
