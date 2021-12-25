using System;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Application.FoodStorages
{
    /// <summary>
    /// Checks access rights of users and storages
    /// </summary>
    public interface IStoragePermissionChecker
    {
        /// <summary>
        /// Checks if the current executing user has read access to a provided storage.
        /// </summary>
        /// <param name="storageId">Identifier of the storage.</param>
        /// <returns>A value indicating whether the user has read access to the storage.</returns>
        Task<bool> UserHasReadAccessAsync(Guid storageId);

        /// <summary>
        /// Checks if the current executing user has write access to a provided storage.
        /// </summary>
        /// <param name="storageId">Identifier of the storage.</param>
        /// <returns>A value indicating whether the user has write access to the storage.</returns>
        Task<bool> UserHasWriteAccessAsync(Guid storageId);
    }
}
