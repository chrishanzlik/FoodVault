using FoodVault.Modules.Storage.Domain.Products;
using FoodVault.Framework.Application.Commands;
using FoodVault.Framework.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Application.Products.CreateProduct
{
    /// <summary>
    /// Command handler for <see cref="CreateProductCommand"/>.
    /// </summary>
    internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand>
    {
        private readonly IProductRepository _productRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateProductCommandHandler" /> class.
        /// </summary>
        /// <param name="productRepository">Product repository.</param>
        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <inheritdoc />
        public async Task<ICommandResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product(request.ProductName, request.Brand, request.Barcode);

            if (request.ImageUploadId.HasValue)
            {
                product.SetProductImage(new FileUploadId(request.ImageUploadId.Value));
            }

            await _productRepository.AddAsync(product);

            return CommandResult.EntityCreated(product.Id);
        }
    }
}
