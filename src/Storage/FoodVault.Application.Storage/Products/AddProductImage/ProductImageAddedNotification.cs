﻿using FoodVault.Application.Events;
using FoodVault.Domain;
using FoodVault.Domain.Storage.Products;
using FoodVault.Domain.Storage.Products.Events;
using Newtonsoft.Json;

namespace FoodVault.Application.Storage.Products.AddProductImage
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
        public ProductImageAddedNotification(ProductImageAddedEvent domainEvent) : base(domainEvent)
        {
            ProductId = domainEvent.ProductId;
            ImageId = domainEvent.ImageId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductImageAddedNotification" /> class.
        /// </summary>
        /// <param name="productId">Product id.</param>
        /// <param name="imageId">Image id.</param>
        [JsonConstructor]
        public ProductImageAddedNotification(ProductId productId, FileUploadId imageId) : base(null)
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