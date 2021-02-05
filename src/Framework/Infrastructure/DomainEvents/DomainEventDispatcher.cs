using Autofac;
using Autofac.Core;
using FoodVault.Framework.Application.Events;
using FoodVault.Framework.Application.Outbox;
using FoodVault.Framework.Domain;
using FoodVault.Framework.Infrastructure.Serialization;
using MediatR;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodVault.Framework.Infrastructure.DomainEvents
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IMediator _mediator;
        private readonly ILifetimeScope _lifeTimeScope;
        private readonly IOutbox _outbox;
        private readonly IDomainEventAccessor _domainEventAccessor;
        private readonly IDomainNotificationsRegistry _domainNotificationsRegistry;

        public DomainEventDispatcher(
            IMediator mediator,
            ILifetimeScope lifeTimeScope,
            IOutbox outbox,
            IDomainEventAccessor domainEventAccessor,
            IDomainNotificationsRegistry domainNotificationsRegistry)
        {
            _mediator = mediator;
            _lifeTimeScope = lifeTimeScope;
            _outbox = outbox;
            _domainEventAccessor = domainEventAccessor;
            _domainNotificationsRegistry = domainNotificationsRegistry;
        }

        public async Task DispatchEventsAsync()
        {
            var domainEventNotifications = new List<IDomainEventNotification<IDomainEvent>>();

            var domainEvents = _domainEventAccessor.GetAllDomainEvents();

            foreach (var domainEvent in domainEvents)
            {
                var notificationType = typeof(IDomainEventNotification<>).MakeGenericType(domainEvent.GetType());
                var notification = _lifeTimeScope.ResolveOptional(notificationType, new List<Parameter>
                {
                    new NamedParameter("domainEvent", domainEvent),
                    new NamedParameter("id", domainEvent.Id)
                });

                if (notification is IDomainEventNotification<IDomainEvent> domainEventNotification)
                {
                    domainEventNotifications.Add(domainEventNotification);
                }
            }

            _domainEventAccessor.ClearAllDomainEvents();

            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent);
            }

            AddNotificationsToOutbox(domainEventNotifications);
        }

        private void AddNotificationsToOutbox(IEnumerable<IDomainEventNotification<IDomainEvent>> notifications)
        {
            foreach(var notification in notifications)
            {
                var type = _domainNotificationsRegistry.GetName(notification.GetType());
                var payload = JsonConvert.SerializeObject(notification, new JsonSerializerSettings
                {
                    ContractResolver = new AllPropertiesContractResolver()
                });

                var outboxMessage = new OutboxMessage(
                    notification.Id,
                    notification.DomainEvent.RaisedAt,
                    type,
                    payload);

                _outbox.Add(outboxMessage);
            }
        }
    }
}
