using FoodVault.Domain.Storage.Product;

namespace FoodVault.Domain.Storage.FoodStore
{
    /// <summary>
    /// <see cref="Product"/> stored in a <see cref="FoodStore"/>.
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
        /// Initializes a new instance of the <see cref="FoodStore" /> class.
        /// </summary>
        /// <param name="foodStoreId">Id of the <see cref="FoodStore"/> where the <see cref="Product.Product"/> should be added.</param>
        /// <param name="productId">Id of the <see cref="Product.Product"/> to add.</param>
        /// <param name="quantity">Amount of items to add.</param>
        public StoredProduct(FoodStoreId foodStoreId, ProductId productId, int quantity)
        {
            FoodStoreId = foodStoreId;
            ProductId = productId;
            Quantity = quantity;
        }

        /// <summary>
        /// Gets the foodstores Id.
        /// </summary>
        public FoodStoreId FoodStoreId { get; }

        /// <summary>
        /// Gets the associated product Id.
        /// </summary>
        public ProductId ProductId { get; }

        /// <summary>
        /// Gets the stored quantity.
        /// </summary>
        public int Quantity { get; internal set; }
    }
}
