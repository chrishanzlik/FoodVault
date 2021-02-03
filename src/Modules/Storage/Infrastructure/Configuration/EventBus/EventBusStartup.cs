using Autofac;
using Autofac.Core;
using FoodVault.Framework.Infrastructure.EventBus;
using Microsoft.Extensions.Logging;

namespace FoodVault.Modules.Storage.Infrastructure.Configuration.EventBus
{
    internal static class EventBusStartup
    {
        public static void Initialize(ILogger logger)
        {
            SubscribeToIntegrationEvents(logger);
        }

        private static void SubscribeToIntegrationEvents(ILogger logger)
        {
            var eventBus = StorageCompositionRoot.BeginLifetimeScope().Resolve<IEventBus>(new TypedParameter(typeof(ILogger), logger));

            // SubscribeToIntegrationEvent<ExampleIntegrationEvent>(eventBus, logger);
            // ...
        }

        private static void SubscribeToIntegrationEvent<T>(IEventBus eventBus, ILogger logger)
            where T : IntegrationEvent
        {
            logger.LogInformation("Subscribe to {@IntegrationEvent}", typeof(T).FullName);
            eventBus.Subscribe(new IntegrationEventGenericHandler<T>());
        }
    }
}
