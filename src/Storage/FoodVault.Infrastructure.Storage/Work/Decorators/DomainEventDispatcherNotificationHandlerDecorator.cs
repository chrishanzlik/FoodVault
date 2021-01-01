using FoodVault.Infrastructure.Work;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Infrastructure.Storage.Work.Decorators
{
    /// <summary>
    /// Notification handler decorator for dispatching domain events.
    /// </summary>
    /// <typeparam name="TNotification">Notification type.</typeparam>
    public class DomainEventDispatcherNotificationHandlerDecorator<TNotification> : INotificationHandler<TNotification>
        where TNotification : INotification
    {
        private readonly INotificationHandler<TNotification> _decorated;

        private readonly IDomainEventDispatcher _domainEventDispatcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEventDispatcherNotificationHandlerDecorator" /> class.
        /// </summary>
        /// <param name="decorated">Decorated notification handler.</param>
        /// <param name="domainEventDispatcher">Domain event dispatcher.</param>
        public DomainEventDispatcherNotificationHandlerDecorator(
            INotificationHandler<TNotification> decorated,
            IDomainEventDispatcher domainEventDispatcher)
        {
            _decorated = decorated;
            _domainEventDispatcher = domainEventDispatcher;
        }

        /// <inheritdoc />
        public async Task Handle(TNotification notification, CancellationToken cancellationToken)
        {
            await _decorated.Handle(notification, cancellationToken);

            await _domainEventDispatcher.DispatchEventsAsync();
        }
    }
}
