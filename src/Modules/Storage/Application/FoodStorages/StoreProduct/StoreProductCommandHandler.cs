using FoodVault.Modules.Storage.Domain.FoodStorages;
using FoodVault.Modules.Storage.Domain.Products;
using FoodVault.Framework.Application.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;
using FoodVault.Framework.Application.DataAccess;

namespace FoodVault.Modules.Storage.Application.FoodStorages.StoreProduct
{
    /// <summary>
    /// Command handler for <see cref="StoreProductCommand"/>.
    /// </summary>
    internal class StoreProductCommandHandler : ICommandHandler<StoreProductCommand>
    {
        private readonly IFoodStorageRepository _foodStorageRepository;
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="StoreProductCommandHandler" /> class.
        /// </summary>
        /// <param name="foodStorageRepository">Food storage repository.</param>
        /// <param name="dbConnectionFactory">DB connection factory.</param>
        public StoreProductCommandHandler(
            IFoodStorageRepository foodStorageRepository,
            IDbConnectionFactory dbConnectionFactory)
        {
            _foodStorageRepository = foodStorageRepository;
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <inheritdoc />
        public async Task<ICommandResult> Handle(StoreProductCommand request, CancellationToken cancellationToken)
        {
            if(!await ProductHelper.CheckProductExistenceAsync(request.ProductId, _dbConnectionFactory))
            {
                return CommandResult.Error(new[] { $"A product with the id '{request.ProductId}' does not exist." });
            }

            var storage = await _foodStorageRepository.GetByIdAsync(new FoodStorageId(request.StorageId));
            if (storage == null)
            {
                return CommandResult.BadParameters(new[] { $"A stroage with the id '{request.StorageId}' does not exist." });
            }

            DateTime? expirationDate = request.ExpirationDate.HasValue ? request.ExpirationDate.Value.Date : (DateTime?)null;

            storage.StoreProduct(new ProductId(request.ProductId), request.Quantity, expirationDate);

            return CommandResult.Ok();
        }
    }
}
