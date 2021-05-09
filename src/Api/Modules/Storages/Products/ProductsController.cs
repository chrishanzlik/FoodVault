using FoodVault.Api.Common;
using FoodVault.Modules.Storage.Application.Products.AddProductImage;
using FoodVault.Modules.Storage.Application.Products.CreateProduct;
using FoodVault.Modules.Storage.Application.Products.GetProductImage;
using FoodVault.Modules.Storage.Application.Products.RemoveProductImage;
using FoodVault.Framework.Application.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FoodVault.Modules.Storage.Application.Contracts;

namespace FoodVault.Api.Modules.Storages.Products
{
    /// <summary>
    /// Interacting with products.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IStorageModule _storageModule;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsController" /> class.
        /// </summary>
        /// <param name="storageModule">Storage module.</param>
        public ProductsController(IStorageModule storageModule)
        {
            _storageModule = storageModule;
        }

        /// <summary>
        /// Creates a product.
        /// </summary>
        /// <param name="request">Create product data.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateProductAsync(CreateProductRequest request)
        {
            var command = new CreateProductCommand(request.ProductName, request.Brand, request.Barcode, request.ImageId);

            ICommandResult result = await _storageModule.ExecuteCommandAsync(command);

            return result.ToActionResult();
        }

        /// <summary>
        /// Uploads and connect an image with a product.
        /// </summary>
        /// <param name="productId">Identifer of the product.</param>
        /// <param name="upload">Image upload.</param>
        /// <returns></returns>
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

            Guid imageId = await _storageModule.StoreFileTemporaryAsync(upload.OpenReadStream(), upload.FileName,
                                                                      upload.ContentType, TimeSpan.FromHours(1));

            var command = new AddProductImageCommand(productId, imageId);

            ICommandResult result = await _storageModule.ExecuteCommandAsync(command);

            return result.ToActionResult();
        }

        /// <summary>
        /// Deletes an product image.
        /// </summary>
        /// <param name="productId">Identifier of the product.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{productId}/image")]
        public async Task<IActionResult> DeleteProductImageAsync([FromRoute] Guid productId)
        {
            var command = new RemoveProductImageCommand(productId);

            ICommandResult result = await _storageModule.ExecuteCommandAsync(command);

            return result.ToActionResult();
        }

        /// <summary>
        /// Gets an product image when available.
        /// </summary>
        /// <param name="productId">Identifier of the product.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{productId}/image")]
        public async Task<IActionResult> GetProductImageAsync([FromRoute] Guid productId)
        {
            var query = new GetProductImageQuery(productId);

            var result = await _storageModule.ExecuteQueryAsync(query);
            if (result == null)
            {
                return NotFound();
            }

            return File(result, result.ContentType, result.FileName);
        }

        private static bool ValidateFileUpload(IFormFile upload, out IEnumerable<string> errors)
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
