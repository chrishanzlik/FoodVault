using Autofac;
using Dapper;
using FoodVault.Framework.Application;
using FoodVault.Framework.Application.FileUploads;
using FoodVault.Framework.Infrastructure.DataAccess;
using FoodVault.Framework.Infrastructure.EventBus;
using FoodVault.Modules.Storage.Application.Contracts;
using FoodVault.Modules.Storage.Application.FoodStorages.CreateStorage;
using FoodVault.Modules.Storage.Application.Products.AddProductImage;
using FoodVault.Modules.Storage.Application.Products.RemoveProductImage;
using FoodVault.Modules.Storage.Infrastructure.Configuration.Application;
using FoodVault.Modules.Storage.Infrastructure.Configuration.DataAccess;
using FoodVault.Modules.Storage.Infrastructure.Configuration.EventBus;
using FoodVault.Modules.Storage.Infrastructure.Configuration.FileUploads;
using FoodVault.Modules.Storage.Infrastructure.Configuration.Logging;
using FoodVault.Modules.Storage.Infrastructure.Configuration.Mediation;
using FoodVault.Modules.Storage.Infrastructure.Configuration.Processing;
using FoodVault.Modules.Storage.Infrastructure.Configuration.Processing.Outbox;
using FoodVault.Modules.Storage.Infrastructure.Configuration.Quartz;
using FoodVault.Modules.Storage.Infrastructure.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace FoodVault.Modules.Storage.Infrastructure.Configuration
{
    internal class StorageStartup
    {
        private static IContainer _container;
        private static ILogger _logger;

        internal static void Initialize(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            IFileUploadSettings fileUploadSettings,
            IStorageModuleUrlBuilder urlBuilder,
            ILogger logger,
            IEventBus eventsBus)
        {
            AddDapperTypeHandlers();

            ConfigureCompositionRoot(
                connectionString,
                executionContextAccessor,
                fileUploadSettings,
                urlBuilder,
                logger,
                eventsBus);

            _logger = logger;

            QuartzStartup.Initialize(logger);
            EventBusStartup.Initialize(logger);
        }

        internal static void Stop()
        {
            QuartzStartup.StopQuartz();
        }

        public static void InitializeDesignTime(string connectionString, ILogger logger)
        {
            AddDapperTypeHandlers();

            ConfigureCompositionRoot(connectionString, null, null, null, logger, null, true);
        }

        private static void ConfigureCompositionRoot(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            IFileUploadSettings fileUploadSettings,
            IStorageModuleUrlBuilder urlBuilder,
            ILogger logger,
            IEventBus eventBus,
            bool isDesignTime = false)
        {
            var domainNotificationRegistrations = new Dictionary<string, Type>();
            domainNotificationRegistrations.Add("StorageCreatedNotification", typeof(StorageCreatedNotification));
            domainNotificationRegistrations.Add("ProductImageRemovedNotification", typeof(ProductImageRemovedNotification));
            domainNotificationRegistrations.Add("ProductImageAddedNotification", typeof(ProductImageAddedNotification));

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule(new LoggingModule(logger));
            containerBuilder.RegisterModule(new QuartzModule());
            containerBuilder.RegisterModule(new DomainModule());
            containerBuilder.RegisterModule(new ApplicationModule());
            containerBuilder.RegisterModule(new ProcessingModule());
            containerBuilder.RegisterModule(new MediatorModule());
            containerBuilder.RegisterModule(new OutboxModule(domainNotificationRegistrations));
            containerBuilder.RegisterModule(new FileUploadModule());
            containerBuilder.RegisterModule(new DataAccessModule(connectionString));
            containerBuilder.RegisterModule(new EventBusModule(eventBus));

            if (isDesignTime)
            {
                _container = containerBuilder.Build();
                StorageCompositionRoot.SetContainer(_container);
            }
            else
            {
                containerBuilder.RegisterInstance(executionContextAccessor);
                containerBuilder.RegisterInstance(fileUploadSettings);
                containerBuilder.RegisterInstance(urlBuilder);

                _container = containerBuilder.Build();
                StorageCompositionRoot.SetContainer(_container);

                MigrateAndSeedDatabase();
            }
        }

        private static void MigrateAndSeedDatabase()
        {
            try
            {
                var context = _container.Resolve<StorageContext>();

                context.Database.Migrate();

                Seed.Apply(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while migrating the database.");
            }
        }

        private static void AddDapperTypeHandlers()
        {
            SqlMapper.AddTypeHandler(new NullableDateTimeUtcDapperHandler());
            SqlMapper.AddTypeHandler(new DateTimeUtcDapperHandler());
        }
    }
}
