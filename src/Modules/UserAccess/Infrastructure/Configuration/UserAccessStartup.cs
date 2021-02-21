using Autofac;
using Dapper;
using FoodVault.Framework.Application;
using FoodVault.Framework.Application.Emails;
using FoodVault.Framework.Application.FileUploads;
using FoodVault.Framework.Infrastructure.DataAccess;
using FoodVault.Framework.Infrastructure.EventBus;
using FoodVault.Modules.UserAccess.Application.Contracts;
using FoodVault.Modules.UserAccess.Application.UserRegistrations.RegisterUser;
using FoodVault.Modules.UserAccess.Infrastructure.Configuration.DataAccess;
using FoodVault.Modules.UserAccess.Infrastructure.Configuration.Domain;
using FoodVault.Modules.UserAccess.Infrastructure.Configuration.EventBus;
using FoodVault.Modules.UserAccess.Infrastructure.Configuration.Logging;
using FoodVault.Modules.UserAccess.Infrastructure.Configuration.Mediation;
using FoodVault.Modules.UserAccess.Infrastructure.Configuration.Processing;
using FoodVault.Modules.UserAccess.Infrastructure.Configuration.Processing.Outbox;
using FoodVault.Modules.UserAccess.Infrastructure.Configuration.Quartz;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace FoodVault.Modules.UserAccess.Infrastructure.Configuration
{
    internal class UserAccessStartup
    {
        private static IContainer _container;
        private static ILogger _logger;

        internal static void Initialize(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            IUserAccessModuleUrlBuilder urlBuilder,
            IEmailSender mailer,
            ILogger logger,
            IEventBus eventsBus)
        {
            AddDapperTypeHandlers();
            
            ConfigureCompositionRoot(
                connectionString,
                executionContextAccessor,
                urlBuilder,
                mailer,
                logger,
                eventsBus);

            _logger = logger;

            QuartzStartup.Initialize(logger);
            EventBusStartup.Initialize(logger);
        }

        internal static void InitializeDesignTime(string connectionString, ILogger<StorageContextFactory> logger)
        {
            AddDapperTypeHandlers();

            ConfigureCompositionRoot(connectionString, null, null, null, logger, null, true);
        }

        internal static void Stop()
        {
            QuartzStartup.StopQuartz();
        }

        private static void ConfigureCompositionRoot(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            IUserAccessModuleUrlBuilder urlBuilder,
            IEmailSender mailer,
            ILogger logger,
            IEventBus eventBus,
            bool isDesignTime = false)
        {
            var domainNotificationRegistrations = new Dictionary<string, Type>();
            domainNotificationRegistrations.Add("UserRegisteredNotification", typeof(UserRegisteredNotification));

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule(new LoggingModule(logger));
            containerBuilder.RegisterModule(new QuartzModule());
            containerBuilder.RegisterModule(new DomainModule());
            containerBuilder.RegisterModule(new ProcessingModule());
            containerBuilder.RegisterModule(new MediatorModule());
            containerBuilder.RegisterModule(new OutboxModule(domainNotificationRegistrations));
            containerBuilder.RegisterModule(new DataAccessModule(connectionString));
            containerBuilder.RegisterModule(new EventBusModule(eventBus));

            if (isDesignTime)
            {
                _container = containerBuilder.Build();
                UserAccessCompositionRoot.SetContainer(_container);
            }
            else
            {
                containerBuilder.RegisterInstance(executionContextAccessor);
                containerBuilder.RegisterInstance(urlBuilder);
                containerBuilder.RegisterInstance(mailer);

                _container = containerBuilder.Build();
                UserAccessCompositionRoot.SetContainer(_container);

                MigrateAndSeedDatabase();
            }
        }

        private static void MigrateAndSeedDatabase()
        {
            try
            {
                var context = _container.Resolve<UserAccessContext>();

                context.Database.Migrate();

                //Seed.Apply(context);
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
