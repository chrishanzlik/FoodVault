using FoodVault.Framework.Application.Commands.Results;
using FoodVault.Modules.Storage.Application.FoodStorages.CreateStorage;
using FoodVault.Modules.Storage.Application.FoodStorages.DeleteStorage;
using FoodVault.Modules.Storage.Application.FoodStorages.GetStoragesForUser;
using FoodVault.Modules.Storage.Application.FoodStorages.GetStorageShares;
using FoodVault.Modules.Storage.Application.FoodStorages.ShareStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FoodVault.Modules.Storage.Tests.IntegrationTests
{
    [Collection("Storage_IntegrationTests")]
    public class DeleteStorageTests : TestBase
    {
        [Fact]
        public async Task DeleteStorage_WhenStorageExists_DeletesStorage()
        {
            var storageName = Guid.NewGuid().ToString();
            var createdResult = await Module.ExecuteCommandAsync(new CreateStorageCommand(storageName, null)) as EntityCreatedCommandResult;
            await Module.ExecuteCommandAsync(new CreateStorageCommand(Guid.NewGuid().ToString(), "This storage should not be deleted."));
            var storages = await Module.ExecuteQueryAsync(new GetStoragesForUserQuery());
            Assert.Equal(2, storages.Count());

            await Module.ExecuteCommandAsync(new DeleteStorageCommand(createdResult.EntityId));
            var storages2 = await Module.ExecuteQueryAsync(new GetStoragesForUserQuery());
            Assert.Single(storages2);
        }

        [Fact]
        public async Task DeleteStorage_WhenStorageExists_RemovesStorageShares()
        {
            var storageName = Guid.NewGuid().ToString();
            var createdResult = await Module.ExecuteCommandAsync(new CreateStorageCommand(storageName, null)) as EntityCreatedCommandResult;
            await Module.ExecuteCommandAsync(new ShareStorageCommand(createdResult.EntityId, Guid.NewGuid(), true));
            var shares = await Module.ExecuteQueryAsync(new GetStorageSharesQuery(createdResult.EntityId));
            Assert.Single(shares);

            await Module.ExecuteCommandAsync(new DeleteStorageCommand(createdResult.EntityId));
            var shares2 = await Module.ExecuteQueryAsync(new GetStorageSharesQuery(createdResult.EntityId));
            Assert.Empty(shares2);
        }
    }
}
