using FoodVault.Framework.Application.FileUploads;
using FoodVault.Framework.Application.Queries;
using System;

namespace FoodVault.Modules.Storage.Application.Products.GetProductImage
{
    public class GetProductImageQuery : IQuery<FileUploadStream>
    {
        public GetProductImageQuery(Guid productId)
        {
            ProductId = productId;
        }

        public Guid ProductId { get; }
    }
}
