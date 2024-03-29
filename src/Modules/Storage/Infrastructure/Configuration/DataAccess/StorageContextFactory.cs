﻿using Autofac;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;

namespace FoodVault.Modules.Storage.Infrastructure.Configuration.DataAccess
{
    /// <summary>
    /// Design time DbContext factory for migrations.
    /// </summary>
    public class StorageContextFactory : IDesignTimeDbContextFactory<StorageContext>
    {
        /// <inheritdoc />
        public StorageContext CreateDbContext(string[] args)
        {
            var logger = new LoggerFactory().CreateLogger<StorageContextFactory>();

            //TODO: Inject Configuration. That's important because the connection string can differ in production env.
            StorageStartup.InitializeDesignTime("Server=.;Database=FoodVault;Trusted_connection=true", logger);
            return StorageCompositionRoot.BeginLifetimeScope().Resolve<StorageContext>();
        }
    }
}
