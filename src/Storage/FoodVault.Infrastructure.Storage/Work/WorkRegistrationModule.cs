using Autofac;
using FoodVault.Application.Events;
using FoodVault.Application.FileUploads;
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
            builder.RegisterType<DashFileNameSanitizer>()
                .As<IFileNameSanitizer>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DomainEventDispatcher>()
                .As<IDomainEventDispatcher>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(Assemblies.Application)
                .AsClosedTypesOf(typeof(IDomainEventNotification<>))
                .InstancePerDependency();

            builder.RegisterType<IsolatedCommandExecutor>()
                .As<ICommandExecutor>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CommandDispatcher>()
                .As<ICommandDispatcher>()
                .InstancePerLifetimeScope();

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
