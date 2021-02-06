using System;
using System.Threading.Tasks;

namespace FoodVault.Framework.Infrastructure.EventBus
{
    /// <summary>
    /// Interface that defines a event bus for bounded context boundary communication.
    /// </summary>
    public interface IEventBus : IDisposable
    {
        /// <summary>
        /// Publishes a new integration event to the event bus.
        /// </summary>
        /// <typeparam name="T">Type of the integration event.</typeparam>
        /// <param name="event">Event to publish.</param>
        /// <returns>Awaitable task.</returns>
        Task Publish<T>(T @event) where T : IntegrationEvent;

        /// <summary>
        /// Subscribes to integration events of another bounded contexts.
        /// </summary>
        /// <typeparam name="T">Type of the integration event to subscribe.</typeparam>
        /// <param name="handler">Handler of the specified integration event.</param>
        void Subscribe<T>(IIntegrationEventHandler<T> handler) where T : IntegrationEvent;

        /// <summary>
        /// Starts event consumation.
        /// </summary>
        void StartConsuming();
    }
}
