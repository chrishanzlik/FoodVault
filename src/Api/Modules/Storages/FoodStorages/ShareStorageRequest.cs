using System;

namespace FoodVault.Api.Modules.Storages.FoodStorages
{
    public class ShareStorageRequest
    {
        public Guid UserId { get; set; }
        public bool WriteAccess { get; set; }
    }
}
