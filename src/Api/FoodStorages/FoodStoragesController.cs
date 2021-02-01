using FoodVault.Api.Common;
using FoodVault.Modules.Storage.Application.FoodStorages.CreateStorage;
using FoodVault.Modules.Storage.Application.FoodStorages.DeleteStorage;
using FoodVault.Modules.Storage.Application.FoodStorages.GetStorageOverview;
using FoodVault.Modules.Storage.Application.FoodStorages.RemoveProduct;
using FoodVault.Modules.Storage.Application.FoodStorages.StoreProduct;
using FoodVault.Framework.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FoodVault.Api.FoodStorages
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodStoragesController : Controller
    {
        private readonly IMediator _mediator;

        public FoodStoragesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetStorageOverviewAsync()
        {
            var query = new GetStorageOverviewQuery();

            var result = await _mediator.Send(query);

            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStorageAsync(CreateStorageRequest request)
        {
            var command = new CreateStorageCommand(request.StorageName, request.Description);

            ICommandResult result = await _mediator.Send(command);

            return result.ToActionResult();
        }

        [HttpDelete("{foodStorageId}")]
        public async Task<IActionResult> DeleteStorageAsync(Guid foodStorageId)
        {
            var command = new DeleteStorageCommand(foodStorageId);

            ICommandResult result = await _mediator.Send(command);

            return result.ToActionResult();
        }

        #region StorageProducts

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

            ICommandResult result = await _mediator.Send(command);

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

            ICommandResult result = await _mediator.Send(command);

            return result.ToActionResult();
        }

        #endregion
    }
}
