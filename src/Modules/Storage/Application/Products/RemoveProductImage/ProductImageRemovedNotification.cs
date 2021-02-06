using FoodVault.Modules.Storage.Domain.Products;
using FoodVault.Modules.Storage.Domain.Products.Events;
using FoodVault.Framework.Application.Events;
using FoodVault.Framework.Domain;
using Newtonsoft.Json;

namespace FoodVault.Modules.Storage.Application.Products.RemoveProductImage
{
    /// <summary>
    /// Notification about a <see cref="ProductImageRemovedEvent"/>. Executes outside of the transaction.
    /// </summary>
    public class ProductImageRemovedNotification : DomainEventNotification<ProductImageRemovedEvent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductImageRemovedNotification" /> class.
        /// </summary>
        /// <param name="domainEvent">Occured domain event.</param>
        public ProductImageRemovedNotification(ProductImageRemovedEvent domainEvent) : base(domainEvent)
        {
            ProductId = domainEvent.ProductId;
            ImageId = domainEvent.ImageId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductImageRemovedNotification" /> class.
        /// </summary>
        /// <param name="productId">Product id.</param>
        /// <param name="imageId">Image id.</param>
        [JsonConstructor]
        public ProductImageRemovedNotification(ProductId productId, FileUploadId imageId) : base(null)
        {
            ProductId = productId;
            ImageId = imageId;
        }

        /// <summary>
        /// Gets the product id.
        /// </summary>
        public ProductId ProductId { get; }

        /// <summary>
        /// Gets the image upload id.
        /// </summary>
        public FileUploadId ImageId { get; }
    }
}
