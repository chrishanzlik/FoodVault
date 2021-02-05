using Autofac;
using FoodVault.Framework.Application.Events;
using FoodVault.Framework.Application.FileUploads;
using FoodVault.Framework.Infrastructure;
using FoodVault.Framework.Infrastructure.DomainEvents;
using FoodVault.Modules.Storage.Infrastructure.FileUploads;
using FoodVault.Modules.Storage.Infrastructure.Work.Decorators;
using MediatR;

namespace FoodVault.Modules.Storage.Infrastructure.Configuration.Processing
{
    /// <summary>
    /// IoC container registrations for 'Work' stuff.
    /// </summary>
    internal class ProcessingModule : Module
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

            builder.RegisterType<DomainEventAccessor>()
                .As<IDomainEventAccessor>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(Assemblies.Application)
                .AsClosedTypesOf(typeof(IDomainEventNotification<>))
                .InstancePerDependency();

            builder.RegisterGenericDecorator(
                typeof(DomainEventDispatcherNotificationHandlerDecorator<>),
                typeof(INotificationHandler<>));

            builder.RegisterGenericDecorator(
                typeof(TransactionCommandHandlerDecorator<>),
                typeof(IRequestHandler<,>));
        }
    }
}
