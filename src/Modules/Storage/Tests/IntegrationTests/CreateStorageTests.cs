using FoodVault.Framework.Application;
using FoodVault.Modules.Storage.Application.FoodStorages.CreateStorage;
using FoodVault.Modules.Storage.Application.FoodStorages.GetStoragesForUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FoodVault.Modules.Storage.Tests.IntegrationTests
{
    [Collection("Storage_IntegrationTests")]
    public class CreateStorageTests : TestBase
    {
        [Fact]
        public async Task CreateStorage_WhenDataIsValid_WillBeCreated()
        {
            string storageName = Guid.NewGuid().ToString();
            string storageDescription = Guid.NewGuid().ToString();

            await Module.ExecuteCommandAsync(new CreateStorageCommand(storageName, storageDescription));

            var storages = await Module.ExecuteQueryAsync(new GetStoragesForUserQuery());

            Assert.NotEmpty(storages);
            
            var storage = storages.SingleOrDefault(x => x.Name.Equals(storageName));

            Assert.NotNull(storage);
            Assert.Equal(storageDescription, storage.Description);
            Assert.Equal(0, storage.Products);
            Assert.Equal(0, storage.ExpiredProducts);
        }

        [Theory]
        [InlineData("")]
        [InlineData("    a ")]
        [InlineData(null)]
        [InlineData("123")]
        public async Task CreateStorage_WhenNameIsInvalid_ThrowsInvalidCommandException(string storageName)
        {
            await Assert.ThrowsAsync<InvalidCommandException>(() =>
            {
                return Module.ExecuteCommandAsync(new CreateStorageCommand(storageName, null));
            });
        }
    }
}
