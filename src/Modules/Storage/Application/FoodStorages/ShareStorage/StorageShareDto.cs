using System;

namespace FoodVault.Modules.Storage.Application.FoodStorages.ShareStorage
{
    public class StorageShareDto
    {
        public Guid UserId { get; set; }
        public Guid StorageId { get; set; }
        public bool WriteAccess { get; set; }
    }
}
