using Dapper;
using FoodVault.Framework.Application;
using FoodVault.Framework.Application.FileUploads;
using FoodVault.Framework.Domain;
using FoodVault.Modules.Storage.Application.Contracts;
using FoodVault.Modules.Storage.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Xunit;

namespace FoodVault.Modules.Storage.Tests.IntegrationTests
{
    public abstract class TestBase : IAsyncLifetime
    {
        protected string ConnectionString { get; private set; }
        protected IStorageModule Module { get; private set; }
        protected ExecutionContextMock ExecutionContext { get; private set; }
        protected IFileUploadSettings FileUploadSettings { get; private set; }
        protected IStorageModuleUrlBuilder UrlBuilder { get; private set; }
        protected ILogger Logger { get; private set; }

        public TestBase()
        {

        }

        public static void AssertBrokenDomainRule<TRule>(Action testCode)
        {
            var exception = Assert.Throws<DomainRuleValidationException>(testCode);
            if (exception != null)
            {
                Assert.IsType<TRule>(exception);
            }
        }

        public async Task InitializeAsync()
        {
            ConnectionString = "Server=.;Database=FoodVault_Test_StorageModule;Trusted_connection=true";
            Logger = Mock.Of<ILogger>();
            ExecutionContext = new ExecutionContextMock(Guid.NewGuid());
            FileUploadSettings = new FileUploadSettingsMock();
            UrlBuilder = new UrlBuilderMock();

            StorageModule.Initialize(
                ConnectionString,
                ExecutionContext,
                FileUploadSettings,
                UrlBuilder,
                Logger,
                null);

            Module = new StorageModule();

            await ResetDatabase();
        }

        public Task DisposeAsync()
        {
            StorageModule.Shutdown();

            return Task.CompletedTask;
        }

        private async Task ResetDatabase()
        {
            const string sql =
                "DELETE FROM [storage].[FileUploads] " +
                "DELETE FROM [storage].[FoodStorages] " +
                "DELETE FROM [storage].[InboxMessages] " +
                "DELETE FROM [storage].[InternalCommands] " +
                "DELETE FROM [storage].[OutboxMessages] " +
                "DELETE FROM [storage].[Products] " +
                "DELETE FROM [storage].[StorageShares] " +
                "DELETE FROM [storage].[StoredProducts];";
            using var con = new SqlConnection(ConnectionString);
            await con.ExecuteScalarAsync(sql);
        }
    }

    public class ExecutionContextMock : IExecutionContextAccessor
    {
        public ExecutionContextMock(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; set; }

        public bool IsAvailable => true;
    }

    public class FileUploadSettingsMock : IFileUploadSettings
    {
        public FileUploadSettingsMock()
        {
            
        }

        public string RootFolder { get; }

        public IEnumerable<string> AllowedExtensions { get; }

        public double MaximumFileSize { get; }
    }

    public class UrlBuilderMock : IStorageModuleUrlBuilder
    {
        public string Url { get; set; }

        public string BuildProductImageUrl(Guid productId)
        {
            return Url;
        }
    }
}
