using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodVault.Domain.Storage.Products
{
    /// <summary>
    /// Interface of a repository for interacting with <see cref="Product"/>s.
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Searches for a <see cref="Product"/> by its id.
        /// </summary>
        /// <param name="id">Id of the <see cref="Product"/> to find.</param>
        /// <returns>The matching <see cref="Product"/> or null if not found.</returns>
        Task<Product> GetByIdAsync(ProductId id);

        /// <summary>
        /// Adds a <see cref="Product"/> to the repository.
        /// </summary>
        /// <param name="product">The product which should be added to the repository.</param>
        /// <returns>Awaitable task.</returns>
        Task AddAsync(Product product);

        /// <summary>
        /// Removes a <see cref="Product"/> with the given id from the repository.
        /// </summary>
        /// <param name="id">Id of the product to remove.</param>
        /// <returns>Awaitable task.</returns>
        Task RemoveAsync(ProductId id);
    }
}
