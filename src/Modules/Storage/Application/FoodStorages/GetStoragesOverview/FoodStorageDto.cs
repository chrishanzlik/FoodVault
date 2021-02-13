using System;

namespace FoodVault.Modules.Storage.Application.FoodStorages.GetStoragesOverview
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

        /// <summary>
        /// Gets the product count of the storage.
        /// </summary>
        public int Products { get; set; }

        /// <summary>
        /// Gets the amount of expired products.
        /// </summary>
        public int ExpiredProducts { get; set; }
    }
}
