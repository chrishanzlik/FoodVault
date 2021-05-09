using Autofac;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;

namespace FoodVault.Modules.UserAccess.Infrastructure.Configuration.DataAccess
{
    /// <summary>
    /// Design time DbContext factory for migrations.
    /// </summary>
    public class StorageContextFactory : IDesignTimeDbContextFactory<UserAccessContext>
    {
        /// <inheritdoc />
        public UserAccessContext CreateDbContext(string[] args)
        {
            var logger = new LoggerFactory().CreateLogger<StorageContextFactory>();

            //TODO: Inject Configuration. That's important because the connection string can differ in production env.
            UserAccessStartup.InitializeDesignTime("Server=.;Database=FoodVault;Trusted_connection=true", logger);
            return UserAccessCompositionRoot.BeginLifetimeScope().Resolve<UserAccessContext>();
        }
    }
}
