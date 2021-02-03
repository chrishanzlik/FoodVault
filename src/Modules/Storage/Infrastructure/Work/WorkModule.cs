using Autofac;
using FoodVault.Modules.Storage.Infrastructure.Work.Decorators;
using FoodVault.Framework.Application.Events;
using FoodVault.Framework.Application.FileUploads;
using FoodVault.Framework.Infrastructure;
using FoodVault.Framework.Infrastructure.Work;
using MediatR;
using FoodVault.Infrastructure.FileUploads;
using FoodVault.Framework.Infrastructure.Domain;

namespace FoodVault.Modules.Storage.Infrastructure.Work
{
    /// <summary>
    /// IoC container registrations for 'Work' stuff.
    /// </summary>
    internal class WorkModule : Module
    {
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<LocalDiskFileStorage>()
                .As<IFileStorage>()
                .InstancePerLifetimeScope();

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
