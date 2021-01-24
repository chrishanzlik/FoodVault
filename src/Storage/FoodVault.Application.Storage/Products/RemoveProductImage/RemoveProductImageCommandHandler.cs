using FoodVault.Application.Commands;
using FoodVault.Domain.Storage.Products;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Application.Storage.Products.AddProductImage
{
    /// <summary>
    /// Command handler for <see cref="RemoveProductImageCommand"/>.
    /// </summary>
    public class RemoveProductImageCommandHandler : ICommandHandler<RemoveProductImageCommand>
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
