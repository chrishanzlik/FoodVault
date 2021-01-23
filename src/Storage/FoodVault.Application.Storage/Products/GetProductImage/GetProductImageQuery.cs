using FoodVault.Application.FileUploads;
using FoodVault.Application.Mediator;
using System;

namespace FoodVault.Application.Storage.Products.GetProductImage
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
