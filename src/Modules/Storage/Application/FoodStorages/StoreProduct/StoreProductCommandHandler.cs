using FoodVault.Modules.Storage.Domain.FoodStorages;
using FoodVault.Modules.Storage.Domain.Products;
using FoodVault.Framework.Application.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;
using FoodVault.Framework.Application.DataAccess;
using FoodVault.Modules.Storage.Domain.Users;

namespace FoodVault.Modules.Storage.Application.FoodStorages.StoreProduct
{
    /// <summary>
    /// Command handler for <see cref="StoreProductCommand"/>.
    /// </summary>
    internal class StoreProductCommandHandler : ICommandHandler<StoreProductCommand>
    {
        private readonly IFoodStorageRepository _foodStorageRepository;
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly IUserContext _userContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="StoreProductCommandHandler" /> class.
        /// </summary>
        /// <param name="foodStorageRepository">Food storage repository.</param>
        /// <param name="dbConnectionFactory">DB connection factory.</param>
        /// <param name="userContext">User context.</param>
        public StoreProductCommandHandler(
            IFoodStorageRepository foodStorageRepository,
            IDbConnectionFactory dbConnectionFactory,
            IUserContext userContext)
        {
            _foodStorageRepository = foodStorageRepository;
            _dbConnectionFactory = dbConnectionFactory;
            _userContext = userContext;
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

            storage.StoreProduct(new ProductId(request.ProductId), request.Quantity, _userContext, expirationDate);

            return CommandResult.Ok();
        }
    }
}
