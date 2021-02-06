using Autofac;
using FoodVault.Framework.Application.Commands;
using FoodVault.Framework.Application.Events;
using FoodVault.Framework.Application.FileUploads;
using FoodVault.Framework.Infrastructure;
using FoodVault.Framework.Infrastructure.DomainEvents;
using FoodVault.Modules.Storage.Infrastructure.FileUploads;
using FoodVault.Modules.Storage.Infrastructure.Work.Decorators;
using MediatR;
using System.Linq;

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
                typeof(TransactionCommandHandlerDecorator<>),
                typeof(ICommandHandler<>));

            builder.RegisterGenericDecorator(
                typeof(ValidationCommandHandlerDecorator<>),
                typeof(ICommandHandler<>));

            // The first matching decorator in the pipeline must unfortunately decorate an
            // IRequestHandler<,>. Thats because of the breaking changes in AutoFac 5/6
            builder.RegisterGenericDecorator(
                typeof(LoggingCommandHandlerDecorator<>),
                typeof(IRequestHandler<,>),
                context =>
                {
                    return context.ImplementationType.GetInterfaces().Any(t =>
                        t.IsGenericType &&
                        t.GetGenericTypeDefinition() == typeof(ICommandHandler<>));
                });

            builder.RegisterGenericDecorator(
                typeof(DomainEventDispatcherNotificationHandlerDecorator<>),
                typeof(INotificationHandler<>));
        }
    }
}
