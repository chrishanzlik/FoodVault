using System;

namespace FoodVault.Modules.Storage.Application.Common
{
    public interface IStorageModuleUrlBuilder
    {
        string BuildProductImageUrl(Guid productId);
    }
}
