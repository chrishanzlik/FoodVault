using System.Threading.Tasks;

namespace FoodVault.Framework.Infrastructure.EventBus
{
    /// <summary>
    /// Interface that handles upcoming <see cref="IntegrationEvent"/>s.
    /// </summary>
    /// <typeparam name="TIntegrationEvent"></typeparam>
    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
        where TIntegrationEvent : IntegrationEvent
    {
        /// <summary>
        /// Handles a upcoming <see cref="IntegrationEvent"/>.
        /// </summary>
        /// <param name="event">Event to handle.</param>
        /// <returns>Awaitable task.</returns>
        Task Handle(TIntegrationEvent @event);
    }

    /// <summary>
    /// Interface that handles upcoming <see cref="IntegrationEvent"/>s.
    /// </summary>
    public interface IIntegrationEventHandler
    {
    }
}
