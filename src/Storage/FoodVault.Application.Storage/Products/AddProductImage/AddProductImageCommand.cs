using FoodVault.Application.Mediator;
using System;

namespace FoodVault.Application.Storage.Products.AddProductImage
{
    /// <summary>
    /// This command adds an product image to a product.
    /// </summary>
    public class AddProductImageCommand : ICommand
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
