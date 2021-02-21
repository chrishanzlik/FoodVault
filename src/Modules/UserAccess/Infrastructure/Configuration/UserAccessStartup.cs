using Dapper;
using FoodVault.Framework.Application;
using FoodVault.Framework.Application.FileUploads;
using FoodVault.Framework.Infrastructure.DataAccess;
using FoodVault.Framework.Infrastructure.EventBus;
using FoodVault.Modules.UserAccess.Application.Contracts;
using Microsoft.Extensions.Logging;

namespace FoodVault.Modules.UserAccess.Infrastructure.Configuration
{
    internal class UserAccessStartup
    {
        internal static void Initialize(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            IFileUploadSettings fileUploadSettings,
            IUserAccessModuleUrlBuilder urlBuilder,
            ILogger logger,
            IEventBus eventsBus)
        {
            AddDapperTypeHandlers();
        }

        internal static void Stop()
        {

        }

        private static void AddDapperTypeHandlers()
        {
            SqlMapper.AddTypeHandler(new NullableDateTimeUtcDapperHandler());
            SqlMapper.AddTypeHandler(new DateTimeUtcDapperHandler());
        }
    }
}
