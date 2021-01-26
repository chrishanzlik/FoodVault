using FoodVault.Domain.Storage.Products;

namespace FoodVault.Domain.Storage.FoodStorages
{
    /// <summary>
    /// Interface for a domain service which checks that a product of a given id exists.
    /// </summary>
    public interface IProductExistsChecker
    {
        /// <summary>
        /// Checks if the product exists.
        /// </summary>
        /// <param name="id">Id of the product.</param>
        /// <returns>True if the product exists, else false.</returns>
        bool ProductExists(ProductId id);
    }
}
