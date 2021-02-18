using FoodVault.Api.Modules.Storages.Products;
using FoodVault.Modules.Storage.Application.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;

namespace FoodVault.Api.Modules.Storages
{
    public class StorageModuleUrlBuilder : IStorageModuleUrlBuilder
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LinkGenerator _linkGenerator;

        public StorageModuleUrlBuilder(IHttpContextAccessor httpContextAccessor, LinkGenerator linkGenerator)
        {
            _httpContextAccessor = httpContextAccessor;
            _linkGenerator = linkGenerator;
        }

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

        private static string NormalizeControllerName(string controller)
        {
            return controller.EndsWith("Controller") ? controller[..^10] : controller;
        }

        private static string NormalizeActionName(string action)
        {
            return action.EndsWith("Async") ? action[..^5] : action;
        }
    }
}
