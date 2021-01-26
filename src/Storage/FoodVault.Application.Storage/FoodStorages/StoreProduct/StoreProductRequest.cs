using System;

namespace FoodVault.Application.Storage.FoodStorages.StoreProduct
{
    /// <summary>
    /// Request to store a product into a given storage.
    /// </summary>
    public class StoreProductRequest
    {
        /// <summary>
        /// Gets or sets the product id.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Gets or sets the product quantity to add.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the expiration date of the product.
        /// </summary>
        public DateTime? ExpirationDate { get; set; }
    }
}
