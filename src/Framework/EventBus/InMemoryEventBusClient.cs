using FoodVault.Framework.Infrastructure.EventBus;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FoodVault.Framework.EventBus
{
    public class InMemoryEventBusClient : IEventBus
    {
        private readonly ILogger _logger;

        public InMemoryEventBusClient(ILogger logger)
        {
            _logger = logger;
        }

        public void Dispose()
        {
        }

        public async Task Publish<T>(T @event)
            where T : IntegrationEvent
        {
            _logger.LogInformation("Publishing {Event}", @event.GetType().FullName);

            await InMemoryEventBus.Instance.Publish(@event);
        }

        public void Subscribe<T>(IIntegrationEventHandler<T> handler)
            where T : IntegrationEvent
        {
            InMemoryEventBus.Instance.Subscribe(handler);
        }

        public void StartConsuming()
        {
        }
    }
}
