using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodVault.Domain.Storage.FoodStore
{
    /// <summary>
    /// Interface of a repository for interacting with <see cref="FoodStore"/>s.
    /// </summary>
    public interface IFoodStoreRepository
    {
        /// <summary>
        /// Searches for a <see cref="FoodStore"/> by its id.
        /// </summary>
        /// <param name="id">Id of the <see cref="FoodStore"/> to find.</param>
        /// <returns>The matching <see cref="FoodStore"/> or null if not found.</returns>
        Task<FoodStore> GetByIdAsync(FoodStoreId id);

        /// <summary>
        /// Adds a <see cref="FoodStore"/> to the repository.
        /// </summary>
        /// <param name="store">The store which should be added to the repository.</param>
        /// <returns>Awaitable task.</returns>
        Task AddAsync(FoodStore store);

        /// <summary>
        /// Removes a <see cref="FoodStore"/> with the given id from the repository.
        /// </summary>
        /// <param name="id">Id of the store to remove.</param>
        /// <returns>Awaitable task.</returns>
        Task RemoveAsync(FoodStoreId id);
    }
}
