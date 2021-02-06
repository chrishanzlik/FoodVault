using FoodVault.Framework.Application.FileUploads;
using FoodVault.Framework.Application.Queries;
using System;

namespace FoodVault.Modules.Storage.Application.Products.GetProductImage
{
    /// <summary>
    /// Query that fetches a product image.
    /// </summary>
    public class GetProductImageQuery : IQuery<FileUploadStream>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetProductImageQuery" /> class.
        /// </summary>
        /// <param name="productId"></param>
        public GetProductImageQuery(Guid productId)
        {
            ProductId = productId;
        }

        /// <summary>
        /// Gets the product identifier.
        /// </summary>
        public Guid ProductId { get; }
    }
}
