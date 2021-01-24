using FoodVault.Domain.Storage.Products.Events;
using System;

namespace FoodVault.Domain.Storage.Products
{
    /// <summary>
    /// Product entity.
    /// </summary>
    public class Product : Entity, IAggregateRoot
    {

        /// <summary>
        /// Required by Entity Framework.
        /// </summary>
        private Product()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Product" /> class.
        /// </summary>
        /// <param name="productName">Products name.</param>
        /// <param name="brand">Brand of the product.</param>
        /// <param name="barcode">Products barcode.</param>
        public Product(
            string productName,
            string brand = null,
            string barcode = null)
        {
            Id = new ProductId(Guid.NewGuid());
            Name = productName;
            Brand = brand;
            Barcode = barcode;

            this.AddDomainEvent(new ProductCreatedEvent(Id));
        }

        /// <summary>
        /// Gets the product id.
        /// </summary>
        public ProductId Id { get; }

        /// <summary>
        /// Gets the associated product image id.
        /// </summary>
        public FileUploadId ImageId { get; private set; }

        /// <summary>
        /// Gets the name of the product.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the barcode of the product.
        /// </summary>
        public string Barcode { get; private set; }

        /// <summary>
        /// Gets the brand of the product.
        /// </summary>
        public string Brand { get; private set; }


        /// <summary>
        /// Adds or replaces the associated image to a product. The old image will be deleted.
        /// </summary>
        /// <param name="imageId">New image id to connect.</param>
        public void SetProductImage(FileUploadId imageId)
        {
            RemoveProductImage();

            this.AddDomainEvent(new ProductImageAddedEvent(Id, imageId));

            ImageId = imageId;
        }

        /// <summary>
        /// Removes the actual connected image from the product.
        /// </summary>
        public void RemoveProductImage()
        {
            if (ImageId == null)
            {
                return;
            }

            this.AddDomainEvent(new ProductImageRemovedEvent(Id, ImageId));

            ImageId = null;
        }
    }
}
