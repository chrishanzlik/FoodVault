using Autofac;
using FoodVault.Modules.Storage.Infrastructure.Work.Decorators;
using FoodVault.Framework.Application.Events;
using FoodVault.Framework.Application.FileUploads;
using FoodVault.Framework.Infrastructure;
using FoodVault.Framework.Infrastructure.Work;
using MediatR;

namespace FoodVault.Modules.Storage.Infrastructure.Work
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

            builder.RegisterGenericDecorator(
                typeof(TransactionCommandHandlerDecorator<>),
                typeof(IRequestHandler<,>));
        }
    }
}
