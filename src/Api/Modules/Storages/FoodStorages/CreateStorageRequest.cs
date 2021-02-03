namespace FoodVault.Api.Modules.Storages.FoodStorages
{
    /// <summary>
    /// Request data for the <see cref="CreateStorageCommand"/>.
    /// </summary>
    public class CreateStorageRequest
    {
        public string StorageName { get; set; }
        public string Description { get; set; }
    }
}
