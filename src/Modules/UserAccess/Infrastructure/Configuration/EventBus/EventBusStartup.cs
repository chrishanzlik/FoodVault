using Autofac;
using FoodVault.Framework.Infrastructure.EventBus;
using Microsoft.Extensions.Logging;

namespace FoodVault.Modules.UserAccess.Infrastructure.Configuration.EventBus
{
    /// <summary>
    /// EventBus startup class.
    /// </summary>
    internal static class EventBusStartup
    {
        /// <summary>
        /// Initializes the event bus.
        /// </summary>
        /// <param name="logger">Application logger.</param>
        public static void Initialize(ILogger logger)
        {
            SubscribeToIntegrationEvents(logger);
        }

        private static void SubscribeToIntegrationEvents(ILogger logger)
        {
            var eventBus = UserAccessCompositionRoot.BeginLifetimeScope().Resolve<IEventBus>(new TypedParameter(typeof(ILogger), logger));

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
