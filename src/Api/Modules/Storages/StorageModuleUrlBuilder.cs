using FoodVault.Api.Modules.Storages.Products;
using FoodVault.Modules.Storage.Application.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;

namespace FoodVault.Api.Modules.Storages
{
    /// <summary>
    /// Builds URLs for the storage module.
    /// </summary>
    internal class StorageModuleUrlBuilder : IStorageModuleUrlBuilder
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LinkGenerator _linkGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageModuleUrlBuilder" /> class.
        /// </summary>
        /// <param name="httpContextAccessor">Http context accessor.</param>
        /// <param name="linkGenerator">Link generator.</param>
        public StorageModuleUrlBuilder(IHttpContextAccessor httpContextAccessor, LinkGenerator linkGenerator)
        {
            _httpContextAccessor = httpContextAccessor;
            _linkGenerator = linkGenerator;
        }

        /// <inheritdoc />
        public string BuildProductImageUrl(Guid productId)
        {
            //TODO: Caching?

            var url = _linkGenerator.GetUriByAction(
                _httpContextAccessor.HttpContext,
                action: NormalizeActionName(nameof(ProductsController.GetProductImageAsync)),
                controller: NormalizeControllerName(nameof(ProductsController)),
                new { productId });

            return url;
        }

        /// <inheritdoc />
        private static string NormalizeControllerName(string controller)
        {
            return controller.EndsWith("Controller") ? controller[..^10] : controller;
        }

        /// <inheritdoc />
        private static string NormalizeActionName(string action)
        {
            return action.EndsWith("Async") ? action[..^5] : action;
        }
    }
}
