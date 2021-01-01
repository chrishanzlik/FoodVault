﻿using Autofac;
using FoodVault.Infrastructure.Storage.Work.Decorators;
using FoodVault.Infrastructure.Work;
using MediatR;

namespace FoodVault.Infrastructure.Storage.Work
{
    /// <summary>
    /// IoC container registrations for 'Work' stuff.
    /// </summary>
    public class WorkRegistrationModule : Module
    {
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DomainEventDispatcher>()
                .As<IDomainEventDispatcher>()
                .InstancePerLifetimeScope();

            //builder.RegisterAssemblyTypes(typeof(SomeNotification).GetTypeInfo().Assembly)
            //    .AsClosedTypesOf(typeof(IDomainEventNotification<>)).InstancePerDependency();

            builder.RegisterGenericDecorator(
                typeof(DomainEventDispatcherNotificationHandlerDecorator<>),
                typeof(INotificationHandler<>));

            //TODO: use mediatr pipelines instead?
            builder.RegisterGenericDecorator(
                typeof(TransactionCommandHandlerDecorator<>),
                typeof(IRequestHandler<,>));
        }
    }
}