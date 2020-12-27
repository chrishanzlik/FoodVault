using FoodVault.Application.Storage.FoodStorages.CreateStorage;
using FoodVault.Application.Storage.FoodStorages.RemoveStorage;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FoodVault.Api.Storage.FoodStorages
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodStorageController : Controller
    {
        private readonly IMediator _mediator;

        public FoodStorageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateStorageAsync(CreateStorageRequest request)
        {
            var command = new CreateStorageCommand(request.StorageName, request.Description);

            var result = await _mediator.Send(command);

            if (result.Success)
            {
                return Created(string.Empty, new { Id = Guid.Empty });
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpDelete("{foodStorageId}")]
        public async Task<IActionResult> RemoveStorageAsync(Guid foodStorageId)
        {
            var command = new RemoveStorageCommand(foodStorageId);

            var result = await _mediator.Send(command);

            //TODO:
            return Ok();
        }
    }
}
