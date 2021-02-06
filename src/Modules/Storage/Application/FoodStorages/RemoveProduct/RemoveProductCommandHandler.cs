using FoodVault.Modules.Storage.Domain.FoodStorages;
using FoodVault.Modules.Storage.Domain.Products;
using FoodVault.Framework.Application.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Application.FoodStorages.RemoveProduct
{
    /// <summary>
    /// Command handler for <see cref="RemoveProductCommand"/>.
    /// </summary>
    internal class RemoveProductCommandHandler : ICommandHandler<RemoveProductCommand>
    {
        private readonly IFoodStorageRepository _foodStorageRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveProductCommandHandler" /> class.
        /// </summary>
        /// <param name="foodStorageRepository">Food storage repository.</param>
        public RemoveProductCommandHandler(IFoodStorageRepository foodStorageRepository)
        {
            _foodStorageRepository = foodStorageRepository;
        }

        /// <inheritdoc />
        public async Task<ICommandResult> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {
            FoodStorageId foodStorageId = new FoodStorageId(request.StorageId);
            ProductId productId = new ProductId(request.ProductId);
            DateTime? expiration = request.ExpirationDate.HasValue ? request.ExpirationDate : (DateTime?)null;

            var storage = await _foodStorageRepository.GetByIdAsync(foodStorageId);
            storage.RemoveProduct(productId, request.Quantity, expiration);

            return CommandResult.Ok();
        }
    }
}
