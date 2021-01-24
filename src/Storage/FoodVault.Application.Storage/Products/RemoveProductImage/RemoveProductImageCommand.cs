using FoodVault.Application.Commands;
using System;

namespace FoodVault.Application.Storage.Products.AddProductImage
{
    /// <summary>
    /// This command removes an product image from a product.
    /// </summary>
    public class RemoveProductImageCommand : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveProductImageCommand" /> class.
        /// </summary>
        /// <param name="productId">Products identifier.</param>
        public RemoveProductImageCommand(Guid productId)
        {
            ProductId = productId;
        }

        /// <summary>
        /// Gets the product id.
        /// </summary>
        public Guid ProductId { get; }
    }
}
