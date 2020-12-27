using Autofac;
using FoodVault.Core.Mediator;
using FoodVault.Infrastructure.Storage.Work.Decorators;
using FoodVault.Infrastructure.Work;

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

            builder.RegisterGenericDecorator(
                typeof(TransactionCommandHandlerDecorator<>),
                typeof(ICommandHandler<>));
        }
    }
}
