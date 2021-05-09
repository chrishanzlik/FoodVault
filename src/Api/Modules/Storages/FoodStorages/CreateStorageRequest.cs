namespace FoodVault.Api.Modules.Storages.FoodStorages
{
    /// <summary>
    /// Request data for storage creation.
    /// </summary>
    public class CreateStorageRequest
    {
        /// <summary>
        /// Gets or sets the storage name.
        /// </summary>
        public string StorageName { get; set; }

        /// <summary>
        /// Gets or sets the storage description.
        /// </summary>
        public string Description { get; set; }
    }
}
