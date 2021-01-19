using FoodVault.Domain.Storage.FoodStorages.Rules;
using FoodVault.Domain.Storage.Products;
using System;

namespace FoodVault.Domain.Storage.FoodStorages
{
    /// <summary>
    /// <see cref="Products"/> stored in a <see cref="StoredProduct"/>.
    /// </summary>
    public class StoredProduct : Entity
    {
        /// <summary>
        /// Required by Entity Framework.
        /// </summary>
        private StoredProduct()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProduct" /> class.
        /// </summary>
        /// <param name="productId">Id of the <see cref="Products.Product"/> to add.</param>
        /// <param name="quantity">Amount of items to add.</param>
        /// <param name="expirationDate">Products expiration date.</param>
        public StoredProduct(ProductId productId, int quantity, DateTime? expirationDate)
        {
            ProductId = productId;
            Quantity = quantity;
            ExpirationDate = expirationDate;
        }

        /// <summary>
        /// Gets the product id.
        /// </summary>
        public ProductId ProductId { get; }

        /// <summary>
        /// Gets the stored quantity.
        /// </summary>
        public int Quantity { get; private set; }

        /// <summary>
        /// Gets the expiration date.
        /// </summary>
        public DateTime? ExpirationDate { get; }

        /// <summary>
        /// Increases the quantity for a given amount.
        /// </summary>
        /// <param name="amount">Value to add.</param>
        internal void IncreaseQuantity(int amount)
        {
            Quantity += amount;
        }

        /// <summary>
        /// Decreases the quantit for a given amount.
        /// </summary>
        /// <param name="amount">Value to subtract.</param>
        internal void DecreaseQuantity(int amount)
        {
            this.CheckDomainRule(new ProductHasEnaughQuantityToRemove(this.Quantity, amount));

            Quantity -= amount;
        }
    }
}
