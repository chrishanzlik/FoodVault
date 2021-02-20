using FoodVault.Modules.Storage.Domain.Users;
using System.Collections.Generic;

namespace FoodVault.Modules.Storage.Domain.FoodStorages
{
    /// <summary>
    /// Finds all users with access to an storage.
    /// </summary>
    public interface IStorageUserSharesFinder
    {
        IEnumerable<UserId> GetSharedUserIdsForStorage(FoodStorageId foodStorageId);
    }
}
