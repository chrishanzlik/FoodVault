using FoodVault.Framework.Infrastructure.EventBus;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FoodVault.Framework.EventBus
{
    /// <summary>
    /// InMemory event bus client.
    /// </summary>
    public class InMemoryEventBusClient : IEventBus
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryEventBusClient" /> class.
        /// </summary>
        /// <param name="logger">Logger instance.</param>
        public InMemoryEventBusClient(ILogger logger)
        {
            _logger = logger;
        }

        /// <inheritdoc />
        public void Dispose()
        {
        }

        /// <inheritdoc />
        public async Task Publish<T>(T @event)
            where T : IntegrationEvent
        {
            _logger.LogInformation("Publishing {Event}", @event.GetType().FullName);

            await InMemoryEventBus.Instance.Publish(@event);
        }

        /// <inheritdoc />
        public void Subscribe<T>(IIntegrationEventHandler<T> handler)
            where T : IntegrationEvent
        {
            InMemoryEventBus.Instance.Subscribe(handler);
        }

        /// <inheritdoc />
        public void StartConsuming()
        {
        }
    }
}
