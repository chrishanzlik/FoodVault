using System;

namespace FoodVault.Application.Storage.Products.CreateProduct
{
    /// <summary>
    /// Request for creating a product.
    /// </summary>
    public class CreateProductRequest
    {
        /// <summary>
        /// Gets or sets the products name.
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the brand of the product.
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Gets or sets the products barcode.
        /// </summary>
        public string Barcode { get; set; }

        /// <summary>
        /// Gets or setes the image id.
        /// </summary>
        public Guid? ImageId { get; set; }
    }
}
