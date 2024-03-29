﻿using FoodVault.Modules.Storage.Domain.Products;
using FoodVault.Framework.Application.Commands;
using FoodVault.Framework.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Application.Products.AddProductImage
{
    /// <summary>
    /// Command handler for <see cref="AddProductImageCommandHandler"/>.
    /// </summary>
    internal class AddProductImageCommandHandler : ICommandHandler<AddProductImageCommand>
    {
        private readonly IProductRepository _productRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddProductImageCommandHandler" /> class.
        /// </summary>
        /// <param name="productRepository">Product repository.</param>
        public AddProductImageCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <inheritdoc />
        public async Task<ICommandResult> Handle(AddProductImageCommand command, CancellationToken cancellationToken)
        {
            ProductId productId = new ProductId(command.ProductId);
            FileUploadId imageId = new FileUploadId(command.ImageId);

            var product = await _productRepository.GetByIdAsync(productId);
            product?.SetProductImage(imageId);

            return CommandResult.Ok();
        }
    }
}
