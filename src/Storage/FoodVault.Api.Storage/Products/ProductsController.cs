using FoodVault.Api.Storage.Common;
using FoodVault.Application.FileUploads;
using FoodVault.Application.Commands;
using FoodVault.Application.Storage.Products.AddProductImage;
using FoodVault.Application.Storage.Products.CreateProduct;
using FoodVault.Application.Storage.Products.GetProductImage;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using System.Linq;

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
            var command = new CreateProductCommand(request.ProductName, request.Brand, request.Barcode, request.ImageId);

            ICommandResult result = await _mediator.Send(command);

            return result.ToActionResult();
        }

        [HttpPost]
        [Route("{productId}/image")]
        public async Task<IActionResult> UploadProductImageAsync([FromRoute] Guid productId, IFormFile upload)
        {
            if (upload == null)
            {
                return BadRequest();
            }

            if (!ValidateFileUpload(upload, out IEnumerable<string> errors))
            {
                return BadRequest(new { errors });
            }

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

        private bool ValidateFileUpload(IFormFile upload, out IEnumerable<string> errors)
        {
            //TODO: Validate with attributes.

            var errorList = new List<string>();
            var allowedExtensions = new string[] { ".jpg", ".png", ".jpeg", ".gif", ".bmp" };
            var maxImageSize = 2 /* MB */ * 1024 * 1024;
            var uploadExtension = Path.GetExtension(upload.FileName).ToLower();

            if (upload.Length > maxImageSize)
            {
                errorList.Add("The image is too large. A maximum size of 2 MB is allowed.");
            }

            if (!allowedExtensions.Contains(uploadExtension))
            {
                errorList.Add("Invalid file extension. Please use common file formats.");
            }

            errors = errorList;
            return errorList.Count == 0;
        }
    }
}
