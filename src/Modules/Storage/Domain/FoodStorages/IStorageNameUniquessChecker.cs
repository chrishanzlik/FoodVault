using System;

namespace FoodVault.Modules.Storage.Domain.FoodStorages
{
    public interface IStorageNameUniquessChecker
    {
        bool IsNameUniqueForUser(string storageName, Guid userId);
    }
}
