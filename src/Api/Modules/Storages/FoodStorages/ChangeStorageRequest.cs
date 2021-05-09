namespace FoodVault.Api.Modules.Storages.FoodStorages
{
    /// <summary>
    /// Request data for changing storages profile data.
    /// </summary>
    public class ChangeStorageRequest
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
