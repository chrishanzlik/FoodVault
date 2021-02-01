using FoodVault.Framework.Domain;

namespace FoodVault.Modules.Storage.Domain.Products.Events
{
    /// <summary>
    /// Domain event which signals that a product was created.
    /// </summary>
    public class ProductCreatedEvent : DomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCreatedEvent" /> class.
        /// </summary>
        /// <param name="productId">Id of the created product.</param>
        public ProductCreatedEvent(ProductId productId)
        {
            ProductId = productId;
        }

        /// <summary>
        /// Gets the id of the product.
        /// </summary>
        public ProductId ProductId { get; }
    }
}
