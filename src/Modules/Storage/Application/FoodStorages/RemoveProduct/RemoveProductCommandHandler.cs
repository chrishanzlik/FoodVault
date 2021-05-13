using FoodVault.Modules.Storage.Domain.FoodStorages;
using FoodVault.Modules.Storage.Domain.Products;
using FoodVault.Framework.Application.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;
using FoodVault.Modules.Storage.Domain.Users;

namespace FoodVault.Modules.Storage.Application.FoodStorages.RemoveProduct
{
    /// <summary>
    /// Command handler for <see cref="RemoveProductCommand"/>.
    /// </summary>
    internal class RemoveProductCommandHandler : ICommandHandler<RemoveProductCommand>
    {
        private readonly IFoodStorageRepository _foodStorageRepository;
        private readonly IUserContext _userContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveProductCommandHandler" /> class.
        /// </summary>
        /// <param name="foodStorageRepository">Food storage repository.</param>
        /// <param name="userContext">User context.</param>
        public RemoveProductCommandHandler(IFoodStorageRepository foodStorageRepository, IUserContext userContext)
        {
            _foodStorageRepository = foodStorageRepository;
            _userContext = userContext;
        }

        /// <inheritdoc />
        public async Task<ICommandResult> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {
            FoodStorageId foodStorageId = new FoodStorageId(request.StorageId);
            ProductId productId = new ProductId(request.ProductId);
            DateTime? expiration = request.ExpirationDate.HasValue ? request.ExpirationDate : (DateTime?)null;

            var storage = await _foodStorageRepository.GetByIdAsync(foodStorageId);
            storage.RemoveProduct(productId, request.Quantity, _userContext, expiration);

            return CommandResult.Ok();
        }
    }
}
