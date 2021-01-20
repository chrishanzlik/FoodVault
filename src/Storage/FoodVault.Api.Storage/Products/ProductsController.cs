using FoodVault.Api.Storage.Common;
using FoodVault.Application.Mediator;
using FoodVault.Application.Storage.Products.AddProductImage;
using FoodVault.Application.Storage.Products.CreateProduct;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FoodVault.Api.Storage.Products
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductAsync(CreateProductRequest request)
        {
            //TODO: Image handling
            var command = new CreateProductCommand(request.ProductName, request.Brand, request.Barcode, null);

            ICommandResult result = await _mediator.Send(command);

            return result.ToActionResult();
        }

        [HttpPost]
        [Route("{productId}/image")]
        public async Task<IActionResult> UploadProductImageAsync([FromRoute] Guid productId, IFormFile upload)
        {
            //TODO: Image handling
            Guid imageId = Guid.Empty;

            var command = new AddProductImageCommand(productId, imageId);

            ICommandResult result = await _mediator.Send(command);

            return result.ToActionResult();
        }

        [HttpDelete]
        [Route("{productId}/image")]
        public async Task<IActionResult> DeleteProductImageAsync([FromRoute] Guid productId)
        {
            var command = new RemoveProductImageCommand(productId);

            ICommandResult result = await _mediator.Send(command);

            return result.ToActionResult();
        }
    }
}
