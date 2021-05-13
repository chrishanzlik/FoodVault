using FoodVault.Api.Common;
using FoodVault.Modules.Storage.Application.FoodStorages.CreateStorage;
using FoodVault.Modules.Storage.Application.FoodStorages.DeleteStorage;
using FoodVault.Modules.Storage.Application.FoodStorages.GetStoragesForUser;
using FoodVault.Modules.Storage.Application.FoodStorages.RemoveProduct;
using FoodVault.Modules.Storage.Application.FoodStorages.StoreProduct;
using FoodVault.Framework.Application.Commands;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using FoodVault.Modules.Storage.Application.Contracts;
using FoodVault.Modules.Storage.Application.FoodStorages.GetStorageContent;
using FoodVault.Modules.Storage.Application.FoodStorages.ChangeStorageProfile;
using FoodVault.Modules.Storage.Application.FoodStorages.ShareStorage;
using FoodVault.Modules.Storage.Application.FoodStorages.UnshareStorage;
using FoodVault.Modules.Storage.Application.FoodStorages.GetStorageShares;
using Microsoft.AspNetCore.Authorization;

namespace FoodVault.Api.Modules.Storages.FoodStorages
{
    /// <summary>
    /// Interacting with storages.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class StoragesController : Controller
    {
        private readonly IStorageModule _storageModule;

        /// <summary>
        /// Initializes a new instance of the <see cref="StoragesController" /> class.
        /// </summary>
        /// <param name="storageModule">Storage module.</param>
        public StoragesController(IStorageModule storageModule)
        {
            _storageModule = storageModule;
        }
        
        /// <summary>
        /// Gets an overview of all available storages.
        /// </summary>
        /// <param name="name">Filter by name</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetStorageOverviewAsync([FromQuery] string name = null)
        {
            var query = new GetStoragesForUserQuery(nameFilter: name?.Trim());

            var result = await _storageModule.ExecuteQueryAsync(query);

            return Ok(result);
        }

        /// <summary>
        /// Creates a new storage.
        /// </summary>
        /// <param name="request">Request data.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateStorageAsync(CreateStorageRequest request)
        {
            var command = new CreateStorageCommand(request.StorageName, request.Description);

            ICommandResult result = await _storageModule.ExecuteCommandAsync(command);

            return result.ToActionResult();
        }

        /// <summary>
        /// Deletes a storage.
        /// </summary>
        /// <param name="storageId">Identifier of the storage.</param>
        /// <returns></returns>
        [HttpDelete("{storageId}")]
        public async Task<IActionResult> DeleteStorageAsync([FromRoute] Guid storageId)
        {
            var command = new DeleteStorageCommand(storageId);

            ICommandResult result = await _storageModule.ExecuteCommandAsync(command);

            return result.ToActionResult();
        }

        /// <summary>
        /// Set storage name and description
        /// </summary>
        /// <param name="storageId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPatch("{storageId}")]
        public async Task<IActionResult> ChangeStorageProfileAsync([FromRoute] Guid storageId, [FromBody] ChangeStorageRequest request)
        {
            var command = new ChangeStorageProfileCommand(storageId, request.StorageName, request.Description);

            ICommandResult result = await _storageModule.ExecuteCommandAsync(command);

            return result.ToActionResult();
        }

        #region StorageProducts

        /// <summary>
        /// Gets all items from a storage.
        /// </summary>
        /// <param name="storageId">Identifier of the storage.</param>
        /// <returns></returns>
        [HttpGet("{storageId}/Products")]
        public async Task<IActionResult> GetStoredProductsAsync([FromRoute] Guid storageId)
        {
            var query = new GetStorageContentQuery(storageId);

            var result = await _storageModule.ExecuteQueryAsync(query);

            return Ok(result);
        }

        /// <summary>
        /// Adds a product to a storage.
        /// </summary>
        /// <param name="storageId">Identifier of the storage.</param>
        /// <param name="request">Identifier of the product.</param>
        /// <returns></returns>
        [HttpPost("{storageId}/Products")]
        public async Task<IActionResult> AddProductsToStorageAsync(
            [FromRoute] Guid storageId,
            [FromBody] StoreProductRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            var command = new StoreProductCommand(storageId, request.ProductId,
                                                  request.Quantity, request.ExpirationDate);

            ICommandResult result = await _storageModule.ExecuteCommandAsync(command);

            return result.ToActionResult();
        }

        /// <summary>
        /// Removes a product from a storage.
        /// </summary>
        /// <param name="storageId">Identifier of the storage.</param>
        /// <param name="productId">Identifier of the product.</param>
        /// <param name="quantity">How many products are taken away</param>
        /// <param name="expiration">Expiration date of the product.</param>
        /// <returns></returns>
        [HttpDelete("{storageId}/Products/{productId}")]
        public async Task<IActionResult> RemoveProductsFromStorageAsync(
            [FromRoute] Guid storageId,
            [FromRoute] Guid productId,
            [FromQuery] int quantity = 1,
            [FromQuery] DateTime? expiration = null)
        {
            var command = new RemoveProductCommand(storageId, productId, quantity, expiration);

            ICommandResult result = await _storageModule.ExecuteCommandAsync(command);

            return result.ToActionResult();
        }

        #endregion

        #region Shares

        /// <summary>
        /// Gets all available storage shares.
        /// </summary>
        /// <param name="storageId">Identifier of the storage.</param>
        /// <returns></returns>
        [HttpGet("{storageId}/Shares")]
        public async Task<IActionResult> GetStorageSharesAsync([FromRoute] Guid storageId)
        {
            var query = new GetStorageSharesQuery(storageId);

            var result = await _storageModule.ExecuteQueryAsync(query);

            return Ok(result);
        }

        /// <summary>
        /// Shares the storage with another user.
        /// </summary>
        /// <param name="storageId">Identifer of the storage.</param>
        /// <param name="request">Share storage data.</param>
        /// <returns></returns>
        [HttpPost("{storageId}/Shares")]
        public async Task<IActionResult> ShareStorageAsync([FromRoute] Guid storageId, [FromBody] ShareStorageRequest request)
        {
            var command = new ShareStorageCommand(storageId, request.UserId, request.WriteAccess);

            ICommandResult result = await _storageModule.ExecuteCommandAsync(command);

            return result.ToActionResult();
        }

        /// <summary>
        /// Unshares the storage for an user.
        /// </summary>
        /// <param name="storageId">Identifier of the storage.</param>
        /// <param name="userId">Identifier of the user.</param>
        /// <returns></returns>
        [HttpDelete("{storageId}/Shares/{userId}")]
        public async Task<IActionResult> UnshareStorageAsync([FromRoute] Guid storageId, [FromRoute] Guid userId)
        {
            var command = new UnshareStorageCommand(storageId, userId);

            ICommandResult result = await _storageModule.ExecuteCommandAsync(command);

            return result.ToActionResult();
        }

        #endregion
    }
}
