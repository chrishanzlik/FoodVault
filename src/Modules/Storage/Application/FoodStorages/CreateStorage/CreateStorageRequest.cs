namespace FoodVault.Modules.Storage.Application.FoodStorages.CreateStorage
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
