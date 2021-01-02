using System;

namespace FoodVault.Application.Storage.FoodStorages.GetStorageOverview
{
    /// <summary>
    /// Food storage data transfer object.
    /// </summary>
    public class FoodStorageDto
    {
        /// <summary>
        /// Gets the identifier of the storage.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets the name of the storage.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the description of the storage.
        /// </summary>
        public string Description { get; set; }
    }
}
