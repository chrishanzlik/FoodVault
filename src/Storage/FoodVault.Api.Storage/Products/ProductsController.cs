using FoodVault.Api.Storage.Common;
using FoodVault.Application.FileUploads;
using FoodVault.Application.Mediator;
using FoodVault.Application.Storage.Products.AddProductImage;
using FoodVault.Application.Storage.Products.CreateProduct;
using FoodVault.Application.Storage.Products.GetProductImage;
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
        private readonly IFileStorage _fileStorage;

        public ProductsController(IMediator mediator, IFileStorage fileStorage)
        {
            _mediator = mediator;
            _fileStorage = fileStorage;
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
            //TODO: validate file

            Guid imageId = await _fileStorage.StoreFileTemporaryAsync(upload.OpenReadStream(), upload.FileName,
                                                                      upload.ContentType, TimeSpan.FromHours(1));

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

        [HttpGet]
        [Route("{productId}/image")]
        public async Task<IActionResult> GetProductImageAsync([FromRoute] Guid productId)
        {
            var query = new GetProductImageQuery(productId);

            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound();
            }

            return File(result, result.ContentType, result.FileName);
        }
    }
}
