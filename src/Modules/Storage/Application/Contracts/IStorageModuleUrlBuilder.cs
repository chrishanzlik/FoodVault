using System;

namespace FoodVault.Modules.Storage.Application.Contracts
{
    public interface IStorageModuleUrlBuilder
    {
        string BuildProductImageUrl(Guid productId);
    }
}
