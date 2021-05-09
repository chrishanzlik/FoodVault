using FoodVault.Modules.Storage.Domain.Products;
using FoodVault.Modules.Storage.Domain.Products.Events;
using FoodVault.Framework.Application.Events;
using FoodVault.Framework.Domain;
using Newtonsoft.Json;
using System;

namespace FoodVault.Modules.Storage.Application.Products.AddProductImage
{
    /// <summary>
    /// Notification about a <see cref="ProductImageAddedEvent"/>. Executes outside of the transaction.
    /// </summary>
    public class ProductImageAddedNotification : DomainEventNotification<ProductImageAddedEvent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductImageAddedNotification" /> class.
        /// </summary>
        /// <param name="domainEvent">Occured domain event.</param>
        /// <param name="id">Notifications id.</param>
        public ProductImageAddedNotification(ProductImageAddedEvent domainEvent, Guid id) : base(domainEvent, id)
        {
            ProductId = domainEvent.ProductId;
            ImageId = domainEvent.ImageId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductImageAddedNotification" /> class.
        /// </summary>
        /// <param name="productId">Product id.</param>
        /// <param name="imageId">Image id.</param>
        /// <param name="id">Notifications id.</param>
        [JsonConstructor]
        public ProductImageAddedNotification(ProductId productId, FileUploadId imageId, Guid id) : base(null, id)
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
