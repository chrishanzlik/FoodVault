namespace FoodVault.Application.Storage.Products
{
    /// <summary>
    /// Product data transfer object.
    /// </summary>
    public class ProductDto
    {
        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the barcode of the product.
        /// </summary>
        public string Barcode { get; set; }

        /// <summary>
        /// Gets or sets the image URL of the product.
        /// </summary>
        public string ImageUrl { get; set; }
    }
}