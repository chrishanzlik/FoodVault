using Autofac;
using Microsoft.EntityFrameworkCore.Design;

namespace FoodVault.Modules.Storage.Infrastructure.Configuration.Database
{
    public class StorageContextFactory : IDesignTimeDbContextFactory<StorageContext>
    {
        public StorageContext CreateDbContext(string[] args)
        {
            StorageStartup.InitializeDesignTime("Server=.;Database=FoodVault;Trusted_connection=true");
            return StorageCompositionRoot.BeginLifetimeScope().Resolve<StorageContext>();
        }
    }
}
