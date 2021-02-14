namespace FoodVault.Modules.Storage.Domain.FoodStorages
{
    public interface IStorageNameUniquessChecker
    {
        bool StorageNameIsUnique(string storageName);
    }
}
