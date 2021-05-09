using Autofac;
using FoodVault.Framework.Application.Events;
using FoodVault.Framework.Application.Outbox;
using FoodVault.Framework.Infrastructure.DomainEvents;
using FoodVault.Modules.UserAccess.Infrastructure.Outbox;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodVault.Modules.UserAccess.Infrastructure.Configuration.Processing.Outbox
{
    internal class OutboxModule : Module
    {
        private readonly Dictionary<string, Type> _notificationsNameTypeMaps;

        public OutboxModule(Dictionary<string, Type> notificationsNameTypeMaps)
        {
            _notificationsNameTypeMaps = notificationsNameTypeMaps;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OutboxAccessor>()
                .As<IOutbox>()
                .InstancePerLifetimeScope();

            CheckDomainNotificationMappings();
            builder.RegisterType<DomainNotificationsRegistry>()
                .As<IDomainNotificationsRegistry>()
                .FindConstructorsWith(new ConstructorFinder())
                .WithParameter("notificationMaps", _notificationsNameTypeMaps)
                .SingleInstance();
        }

        private void CheckDomainNotificationMappings()
        {
            var assemblyNotifications = Assemblies.Application
                .GetTypes()
                .Where(x => x.GetInterfaces().Contains(typeof(IDomainEventNotification)))
                .ToList();
            var mappedNotifications = _notificationsNameTypeMaps.Values;

            var notMappedNotifications = assemblyNotifications.Where(x => !mappedNotifications.Contains(x));

            if (notMappedNotifications.Any())
            {
                var notMappedString = notMappedNotifications
                    .Select(x => x.FullName)
                    .Aggregate((a, b) => $"{a},{b}");

                var message =
                    "The following DomainEventNotifications are not registered inside the 'IDomainNotificationsRegistry': " +
                    notMappedString +
                    ". Registration should take place inside the UserAccessStartup class.";

                throw new ApplicationException(message);
            }
        }
    }
}
