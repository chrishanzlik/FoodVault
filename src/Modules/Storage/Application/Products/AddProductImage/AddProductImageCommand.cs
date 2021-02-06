using FoodVault.Framework.Application.Commands;
using System;

namespace FoodVault.Modules.Storage.Application.Products.AddProductImage
{
    /// <summary>
    /// This command adds an product image to a product.
    /// </summary>
    public class AddProductImageCommand : Command
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddProductImageCommand" /> class.
        /// </summary>
        /// <param name="productId">Products identifier.</param>
        /// <param name="imageId">Image uploads identifier.</param>
        public AddProductImageCommand(Guid productId, Guid imageId)
        {
            ProductId = productId;
            ImageId = imageId;
        }

        /// <summary>
        /// Gets the product id.
        /// </summary>
        public Guid ProductId { get; }

        /// <summary>
        /// Gets the image upload id.
        /// </summary>
        public Guid ImageId { get; }
    }
}
