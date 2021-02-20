using System;

namespace FoodVault.Modules.Storage.Application.FoodStorages.GetStorageShares
{
    public class StorageShareDto
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public DateTime SharedAt { get; set; }
    }
}
