using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodVault.Domain.Storage.FoodStorages
{
    /// <summary>
    /// Interface of a repository for interacting with <see cref="FoodStorage"/>s.
    /// </summary>
    public interface IFoodStoreRepository
    {
        /// <summary>
        /// Searches for a <see cref="FoodStorage"/> by its id.
        /// </summary>
        /// <param name="id">Id of the <see cref="FoodStorage"/> to find.</param>
        /// <returns>The matching <see cref="FoodStorage"/> or null if not found.</returns>
        Task<FoodStorage> GetByIdAsync(FoodStorageId id);

        /// <summary>
        /// Adds a <see cref="FoodStorage"/> to the repository.
        /// </summary>
        /// <param name="store">The store which should be added to the repository.</param>
        /// <returns>Awaitable task.</returns>
        Task AddAsync(FoodStorage store);

        /// <summary>
        /// Removes a <see cref="FoodStorage"/> with the given id from the repository.
        /// </summary>
        /// <param name="id">Id of the store to remove.</param>
        /// <returns>Awaitable task.</returns>
        Task RemoveAsync(FoodStorageId id);
    }
}
