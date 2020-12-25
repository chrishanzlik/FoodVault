using FoodVault.Domain.Storage.Product;

namespace FoodVault.Domain.Storage.FoodStore.Events
{
    /// <summary>
    /// Domain event which signals that a product has been added to a store.
    /// </summary>
    public class ProductStoredEvent : DomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductStoredEvent" /> class.
        /// </summary>
        /// <param name="storeId">Id of the <see cref="FoodStore"/>.</param>
        /// <param name="productId">Id of the <see cref="Product.Product"/>.</param>
        /// <param name="quantity">Products quantity.</param>
        public ProductStoredEvent(FoodStoreId storeId, ProductId productId, int quantity)
        {
            FoodStoreId = storeId;
            ProductId = productId;
            Quantity = quantity;
        }

        /// <summary>
        /// Gets the id of the <see cref="FoodStore"/> where the product was added.
        /// </summary>
        public FoodStoreId FoodStoreId { get; }

        /// <summary>
        /// Gets the id of the added product.
        /// </summary>
        public ProductId ProductId { get; }

        /// <summary>
        /// Gets the quantity of added products.
        /// </summary>
        public int Quantity { get; }
    }
}
