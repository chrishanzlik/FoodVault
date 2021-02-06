using FoodVault.Modules.Storage.Domain.Products;
using FoodVault.Framework.Application.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Application.Products.RemoveProductImage
{
    /// <summary>
    /// Command handler for <see cref="RemoveProductImageCommand"/>.
    /// </summary>
    internal class RemoveProductImageCommandHandler : ICommandHandler<RemoveProductImageCommand>
    {
        private readonly IProductRepository _productRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveProductImageCommandHandler" /> class.
        /// </summary>
        /// <param name="productRepository">Product repository.</param>
        public RemoveProductImageCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <inheritdoc />
        public async Task<ICommandResult> Handle(RemoveProductImageCommand command, CancellationToken cancellationToken)
        {
            ProductId productId = new ProductId(command.ProductId);

            var product = await _productRepository.GetByIdAsync(productId);
            product?.RemoveProductImage();

            return CommandResult.Ok();
        }
    }
}
