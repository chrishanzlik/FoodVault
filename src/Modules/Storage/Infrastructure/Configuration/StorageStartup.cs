using Autofac;
using FoodVault.Framework.Application;
using FoodVault.Framework.Application.FileUploads;
using FoodVault.Framework.Infrastructure.EventBus;
using FoodVault.Modules.Storage.Infrastructure.Configuration.Database;
using FoodVault.Modules.Storage.Infrastructure.Configuration.EventBus;
using FoodVault.Modules.Storage.Infrastructure.Configuration.Quartz;
using FoodVault.Modules.Storage.Infrastructure.Domain;
using FoodVault.Modules.Storage.Infrastructure.Work;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodVault.Modules.Storage.Infrastructure.Configuration
{
    public class StorageStartup
    {
        private static IContainer _container;
        private static ILogger _logger;

        public static void Initialize(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            IFileUploadSettings fileUploadSettings,
            ILogger logger,
            IEventBus eventsBus)
        {
            //var moduleLogger = logger.ForContext("Module", "Meetings");

            ConfigureCompositionRoot(
                connectionString,
                executionContextAccessor,
                fileUploadSettings,
                logger,
                eventsBus);

            _logger = logger;

            QuartzStartup.Initialize(logger);
            EventBusStartup.Initialize(logger);
        }

        public static void Stop()
        {
            QuartzStartup.StopQuartz();
        }

        public static void InitializeDesignTime(string connectionString)
        {
            ConfigureCompositionRoot(connectionString, null, null, null, null, true);
        }

        private static void ConfigureCompositionRoot(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            IFileUploadSettings fileUploadSettings,
            ILogger logger,
            IEventBus eventBus,
            bool isDesignTime = false)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new QuartzModule());
            containerBuilder.RegisterModule(new DomainModule());
            containerBuilder.RegisterModule(new MediatorModule());
            containerBuilder.RegisterModule(new WorkModule());
            containerBuilder.RegisterModule(new DatabaseModule(connectionString));
            containerBuilder.RegisterModule(new EventBusModule(eventBus));

            //containerBuilder.RegisterModule(new LoggingModule(logger.ForContext("Module", "Meetings")));

            //var loggerFactory = new SerilogLoggerFactory(logger);
            //containerBuilder.RegisterModule(new DataAccessModule(connectionString, loggerFactory));
            //containerBuilder.RegisterModule(new ProcessingModule());
            //containerBuilder.RegisterModule(new EventsBusModule(eventsBus));
            //containerBuilder.RegisterModule(new MediatorModule());
            //containerBuilder.RegisterModule(new AuthenticationModule());

            //var domainNotificationsMap = new BiDictionary<string, Type>();
            //domainNotificationsMap.Add("MeetingGroupProposalAcceptedNotification", typeof(MeetingGroupProposalAcceptedNotification));
            //domainNotificationsMap.Add("MeetingGroupProposedNotification", typeof(MeetingGroupProposedNotification));
            //domainNotificationsMap.Add("MeetingGroupCreatedNotification", typeof(MeetingGroupCreatedNotification));
            //domainNotificationsMap.Add("MeetingAttendeeAddedNotification", typeof(MeetingAttendeeAddedNotification));
            //domainNotificationsMap.Add("MemberCreatedNotification", typeof(MemberCreatedNotification));
            //domainNotificationsMap.Add("MemberSubscriptionExpirationDateChangedNotification", typeof(MemberSubscriptionExpirationDateChangedNotification));
            //domainNotificationsMap.Add("MeetingCommentLikedNotification", typeof(MeetingCommentLikedNotification));
            //containerBuilder.RegisterModule(new OutboxModule(domainNotificationsMap));

            if (isDesignTime)
            {
                _container = containerBuilder.Build();
                StorageCompositionRoot.SetContainer(_container);
            }
            else
            {
                containerBuilder.RegisterInstance(executionContextAccessor);
                containerBuilder.RegisterInstance(fileUploadSettings);

                _container = containerBuilder.Build();
                StorageCompositionRoot.SetContainer(_container);

                MigrateAndSeedDatabase();
            }
        }

        static void MigrateAndSeedDatabase()
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
    }
}
