namespace FoodVault.Domain.Storage.Products.Events
{
    /// <summary>
    /// Domain event which signals that a product image was added.
    /// </summary>
    public class ProductImageAddedEvent : DomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductImageAddedEvent" /> class.
        /// </summary>
        /// <param name="productId">Id of the product.</param>
        /// <param name="imageId">Id of the image.</param>
        public ProductImageAddedEvent(ProductId productId, FileUploadId imageId)
        {
            ProductId = productId;
            ImageId = imageId;
        }

        /// <summary>
        /// Gets the id of the product.
        /// </summary>
        public ProductId ProductId { get; }

        /// <summary>
        /// Gets the id of the image.
        /// </summary>
        public FileUploadId ImageId { get; }
    }
}
