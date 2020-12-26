using FoodVault.Domain.Storage.FoodStorages.Rules;
using FoodVault.Domain.Storage.Products;

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
        public StoredProduct(ProductId productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
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
