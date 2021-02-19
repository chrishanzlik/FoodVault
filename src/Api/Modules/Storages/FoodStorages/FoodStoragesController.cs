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

namespace FoodVault.Api.Modules.Storages.FoodStorages
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodStoragesController : Controller
    {
        private readonly IStorageModule _storageModule;

        public FoodStoragesController(IStorageModule storageModule)
        {
            _storageModule = storageModule;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetStorageOverviewAsync([FromQuery] string name = null)
        {
            var query = new GetStoragesForUserQuery(nameFilter: name?.Trim());

            var result = await _storageModule.ExecuteQueryAsync(query);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStorageAsync(CreateStorageRequest request)
        {
            var command = new CreateStorageCommand(request.StorageName, request.Description);

            ICommandResult result = await _storageModule.ExecuteCommandAsync(command);

            return result.ToActionResult();
        }

        [HttpDelete("{foodStorageId}")]
        public async Task<IActionResult> DeleteStorageAsync([FromRoute] Guid foodStorageId)
        {
            var command = new DeleteStorageCommand(foodStorageId);

            ICommandResult result = await _storageModule.ExecuteCommandAsync(command);

            return result.ToActionResult();
        }

        [HttpPatch("{foodStorageId}")]
        public async Task<IActionResult> ChangeStorageProfileAsync([FromRoute] Guid foodStorageId, [FromBody] ChangeStorageRequest request)
        {
            var command = new ChangeStorageProfileCommand(foodStorageId, request.StorageName, request.Description);

            ICommandResult result = await _storageModule.ExecuteCommandAsync(command);

            return result.ToActionResult();
        }

        #region StorageProducts

        [HttpGet("{foodStorageId}/products")]
        public async Task<IActionResult> GetStoredProductsAsync([FromRoute] Guid foodStorageId)
        {
            var query = new GetStorageContentQuery(foodStorageId);

            var result = await _storageModule.ExecuteQueryAsync(query);

            return Ok(result);
        }

        [HttpPost("{foodStorageId}/products")]
        public async Task<IActionResult> AddProductsToStorageAsync(
            [FromRoute] Guid foodStorageId,
            [FromBody] StoreProductRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            var command = new StoreProductCommand(foodStorageId, request.ProductId,
                                                  request.Quantity, request.ExpirationDate);

            ICommandResult result = await _storageModule.ExecuteCommandAsync(command);

            return result.ToActionResult();
        }

        [HttpDelete("{foodStorageId}/products/{productId}")]
        public async Task<IActionResult> RemoveProductsFromStorageAsync(
            [FromRoute] Guid foodStorageId,
            [FromRoute] Guid productId,
            [FromQuery] int quantity = 1,
            [FromQuery] DateTime? expiration = null)
        {
            var command = new RemoveProductCommand(foodStorageId, productId, quantity, expiration);

            ICommandResult result = await _storageModule.ExecuteCommandAsync(command);

            return result.ToActionResult();
        }

        #endregion
    }
}
