namespace FoodVault.Api.Modules.Storages.FoodStorages
{
    /// <summary>
    /// Request data for changing storages profile data.
    /// </summary>
    public class ChangeStorageRequest
    {
        public string StorageName { get; set; }
        public string Description { get; set; }
    }
}
