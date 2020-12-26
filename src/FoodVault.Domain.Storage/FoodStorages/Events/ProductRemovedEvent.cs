using FoodVault.Domain.Storage.Products;

namespace FoodVault.Domain.Storage.FoodStorages.Events
{
    /// <summary>
    /// Domain event which signals that a product has been removed from a storage.
    /// </summary>
    public class ProductRemovedEvent : DomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRemovedEvent" /> class.
        /// </summary>
        /// <param name="storageId">Id of the <see cref="FoodStorage"/>.</param>
        /// <param name="productId">Id of the <see cref="Products.Product"/>.</param>
        /// <param name="quantity">Products quantity.</param>
        public ProductRemovedEvent(FoodStorageId storageId, ProductId productId, int quantity)
        {
            FoodStorageId = storageId;
            ProductId = productId;
            Quantity = quantity;
        }

        /// <summary>
        /// Gets the id of the <see cref="FoodStorage"/> where the product was removed.
        /// </summary>
        public FoodStorageId FoodStorageId { get; }

        /// <summary>
        /// Gets the id of the removed product.
        /// </summary>
        public ProductId ProductId { get; }

        /// <summary>
        /// Gets the quantity of removed products.
        /// </summary>
        public int Quantity { get; }
    }
}
