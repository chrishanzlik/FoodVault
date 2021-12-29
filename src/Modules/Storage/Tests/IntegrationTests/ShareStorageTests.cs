using FoodVault.Framework.Application.Commands.Results;
using FoodVault.Modules.Storage.Application.FoodStorages.CreateStorage;
using FoodVault.Modules.Storage.Application.FoodStorages.GetStoragesForUser;
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
    public class ShareStorageTests : TestBase
    {
        [Fact]
        public async Task ShareStorage_WhenUserIdIsValid_WillGrantReadAccess()
        {
            var ownerUserId = Guid.NewGuid();
            var shareTargetUserId = Guid.NewGuid();
            var storageName = Guid.NewGuid().ToString();

            ExecutionContext.UserId = ownerUserId;

            var createdResult = await Module.ExecuteCommandAsync(new CreateStorageCommand(storageName, null)) as EntityCreatedCommandResult;
            await Module.ExecuteCommandAsync(new CreateStorageCommand(Guid.NewGuid().ToString(), "This should not be shared."));

            ExecutionContext.UserId = shareTargetUserId;
            var expectedEmptyResult = await Module.ExecuteQueryAsync(new GetStoragesForUserQuery());
            Assert.Empty(expectedEmptyResult);

            ExecutionContext.UserId = ownerUserId;
            await Module.ExecuteCommandAsync(new ShareStorageCommand(createdResult.EntityId, shareTargetUserId, false));

            ExecutionContext.UserId = shareTargetUserId;
            var result = await Module.ExecuteQueryAsync(new GetStoragesForUserQuery());

            Assert.NotEmpty(result);
            var expected = result.Single();
            Assert.Equal(storageName, expected.Name);
            Assert.Equal(ownerUserId, expected.OwnerId);
        }
    }
}
