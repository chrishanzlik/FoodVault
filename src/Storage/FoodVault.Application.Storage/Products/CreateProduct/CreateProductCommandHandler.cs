using FoodVault.Application.Commands;
using FoodVault.Domain;
using FoodVault.Domain.Storage.Products;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Application.Storage.Products.CreateProduct
{
    /// <summary>
    /// Command handler for <see cref="CreateProductCommand"/>.
    /// </summary>
    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand>
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
