using FoodVault.Modules.Storage.Domain.Products;
using FoodVault.Framework.Domain;

namespace FoodVault.Modules.Storage.Domain.FoodStorages.Events
{
    /// <summary>
    /// Domain event which signals that a product has been added to a storage.
    /// </summary>
    public class ProductStoredEvent : DomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductStoredEvent" /> class.
        /// </summary>
        /// <param name="storageId">Identifier of the <see cref="FoodStorage"/>.</param>
        /// <param name="productId">Identifier of the <see cref="Products.Product"/>.</param>
        /// <param name="quantity">Products quantity.</param>
        public ProductStoredEvent(FoodStorageId storageId, ProductId productId, int quantity)
        {
            FoodStorageId = storageId;
            ProductId = productId;
            Quantity = quantity;
        }

        /// <summary>
        /// Gets the id of the <see cref="FoodStorage"/> where the product was added.
        /// </summary>
        public FoodStorageId FoodStorageId { get; }

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
