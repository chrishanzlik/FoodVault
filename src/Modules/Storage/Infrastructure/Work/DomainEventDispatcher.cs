using Autofac;
using Autofac.Core;
using FoodVault.Modules.Storage.Infrastructure.Configuration.DataAccess;
using FoodVault.Framework.Application.Events;
using FoodVault.Framework.Domain;
using FoodVault.Framework.Infrastructure.Outbox;
using MediatR;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodVault.Framework.Infrastructure;

namespace FoodVault.Modules.Storage.Infrastructure.Work
{
    /// <summary>
    /// Dispatches all domain events within a transaction.
    /// </summary>
    internal class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly StorageContext _storageContext;
        private readonly IMediator _mediator;
        private readonly ILifetimeScope _scope;

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEventDispatcher" /> class.
        /// </summary>
        /// <param name="storageContext">Database context.</param>
        /// <param name="mediator">Mediator.</param>
        /// <param name="scope">Transaction scope.</param>
        public DomainEventDispatcher(
            StorageContext storageContext,
            IMediator mediator,
            ILifetimeScope scope)
        {
            _storageContext = storageContext;
            _mediator = mediator;
            _scope = scope;
        }

        /// <inheritdoc />
        public async Task DispatchEventsAsync()
        {
            var entities = _storageContext.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents?.Any() == true)
                .ToList();

            var events = entities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            var domainEventNotifications = new List<IDomainEventNotification<IDomainEvent>>();

            foreach (var domainEvent in events)
            {
                var notificationType = typeof(IDomainEventNotification<>);
                var genericNotificationType = notificationType.MakeGenericType(domainEvent.GetType());

                var domainNotification = _scope.ResolveOptional(genericNotificationType, new List<Parameter>
                {
                    new NamedParameter("domainEvent", domainEvent)
                });

                if (domainNotification != null)
                {
                    domainEventNotifications.Add(domainNotification as IDomainEventNotification<IDomainEvent>);
                }
            }
            
            entities.ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = events.Select(x => _mediator.Publish(x));

            await Task.WhenAll(tasks);

            AddNotificationsToOutbox(domainEventNotifications);
        }

        private void AddNotificationsToOutbox(IEnumerable<IDomainEventNotification<IDomainEvent>> notifications)
        {
            foreach (var domainEventNotification in notifications)
            {
                string type = domainEventNotification.GetType().FullName;
                string payload = JsonConvert.SerializeObject(domainEventNotification);

                var outboxMessage = new OutboxMessage(
                    domainEventNotification.DomainEvent.RaisedAt,
                    type,
                    payload);

                _storageContext.OutboxMessages.Add(outboxMessage);
            }
        }
    }
}
