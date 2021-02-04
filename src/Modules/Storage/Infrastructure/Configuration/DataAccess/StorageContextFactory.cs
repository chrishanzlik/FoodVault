using Autofac;
using Microsoft.EntityFrameworkCore.Design;

namespace FoodVault.Modules.Storage.Infrastructure.Configuration.DataAccess
{
    public class StorageContextFactory : IDesignTimeDbContextFactory<StorageContext>
    {
        public StorageContext CreateDbContext(string[] args)
        {
            //TODO: Inject Configuration. That's important because the connection string can differ in production env.
            StorageStartup.InitializeDesignTime("Server=.;Database=FoodVault;Trusted_connection=true");
            return StorageCompositionRoot.BeginLifetimeScope().Resolve<StorageContext>();
        }
    }
}
