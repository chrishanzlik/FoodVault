using FoodVault.Application.Commands;
using System;

namespace FoodVault.Application.Storage.FoodStorages.StoreProduct
{
    /// <summary>
    /// Stores a product to a food storage.
    /// </summary>
    public class StoreProductCommand : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StoreProductCommand" /> class.
        /// </summary>
        /// <param name="storageId">Storage identifier.</param>
        /// <param name="productId">Product identifier.</param>
        /// <param name="quantity">Quantity to add.</param>
        /// <param name="expirationDate">Expiration date of the product.</param>
        public StoreProductCommand(Guid storageId, Guid productId, int quantity, DateTime? expirationDate)
        {
            StorageId = storageId;
            ProductId = productId;
            Quantity = quantity;
            ExpirationDate = expirationDate;
        }

        /// <summary>
        /// Gets the storage id.
        /// </summary>
        public Guid StorageId { get; }

        /// <summary>
        /// Gets the product id.
        /// </summary>
        public Guid ProductId { get; }

        /// <summary>
        /// Gets the product quantity to add.
        /// </summary>
        public int Quantity { get; }

        /// <summary>
        /// Gets the expiration date of the product.
        /// </summary>
        public DateTime? ExpirationDate { get; }
    }
}
