﻿using FoodVault.Application.Commands;
using FoodVault.Domain.Storage.FoodStorages;
using FoodVault.Domain.Storage.Products;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Application.Storage.FoodStorages.StoreProduct
{
    /// <summary>
    /// Command handler for <see cref="StoreProductCommand"/>.
    /// </summary>
    public class StoreProductCommandHandler : ICommandHandler<StoreProductCommand>
    {
        private readonly IFoodStorageRepository _foodStorageRepository;
        private readonly IProductExistsChecker _productExistsChecker;

        /// <summary>
        /// Initializes a new instance of the <see cref="StoreProductCommandHandler" /> class.
        /// </summary>
        /// <param name="foodStorageRepository">Food storage repository.</param>
        /// <param name="productExistsChecker">Domain service that checks if a product exists.</param>
        public StoreProductCommandHandler(
            IFoodStorageRepository foodStorageRepository,
            IProductExistsChecker productExistsChecker)
        {
            _foodStorageRepository = foodStorageRepository;
            _productExistsChecker = productExistsChecker;
        }

        /// <inheritdoc />
        public async Task<ICommandResult> Handle(StoreProductCommand request, CancellationToken cancellationToken)
        {
            FoodStorageId storageId = new FoodStorageId(request.StorageId);
            ProductId productId = new ProductId(request.ProductId);
            DateTime? date = request.ExpirationDate.HasValue ? request.ExpirationDate.Value.Date : (DateTime?)null;

            var storage = await _foodStorageRepository.GetByIdAsync(storageId);
            storage?.StoreProduct(productId, request.Quantity, date, _productExistsChecker);

            return CommandResult.Ok();
        }
    }
}