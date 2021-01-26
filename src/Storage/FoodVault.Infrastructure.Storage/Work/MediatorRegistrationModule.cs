using Autofac;
using Autofac.Core;
using Autofac.Core.Activators.Reflection;
using Autofac.Features.Variance;
using FluentValidation;
using FoodVault.Application.FileUploads;
using FoodVault.Application.Storage.FoodStorages.CreateStorage;
using FoodVault.Application.Validation;
using FoodVault.Infrastructure.InternalCommands;
using FoodVault.Infrastructure.Outbox;
using MediatR;
using MediatR.Pipeline;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FoodVault.Infrastructure.Storage.Work
{
    /// <summary>
    /// Registrations for 'Mediator' module.
    /// </summary>
    public class MediatorRegistrationModule : Autofac.Module
    {
        private static readonly Type[] mediatrOpenTypes = new[]
        {
            typeof(IRequestHandler<,>),
            typeof(INotificationHandler<>),
            typeof(IValidator<>),
        };

        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterSource(new ScopedContravariantRegistrationSource(mediatrOpenTypes));

            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

            foreach (var mediatrOpenType in mediatrOpenTypes)
            {
                builder
                    .RegisterAssemblyTypes(Assemblies.Application)
                    .AsClosedTypesOf(mediatrOpenType)
                    .FindConstructorsWith(new AllConstructorFinder())
                    .AsImplementedInterfaces();
            }

            builder.RegisterType<ProcessOutboxCommandHandler>()
                .AsImplementedInterfaces()
                .WithParameter("commandsAssembly", Assemblies.Application)
                .InstancePerLifetimeScope();

            builder.RegisterType<ProcessInternalCommandsCommandHandler>()
                .AsImplementedInterfaces()
                .WithParameter("commandsAssembly", Assemblies.Application)
                .InstancePerLifetimeScope();

            builder.RegisterType<RemoveExpiredFilesCommandHandler>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(CommandValidationPipelineBehavior<>)).As(typeof(IPipelineBehavior<,>));

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
        }

        private class ScopedContravariantRegistrationSource : IRegistrationSource
        {
            private readonly IRegistrationSource _source = new ContravariantRegistrationSource();
            private readonly List<Type> _types = new List<Type>();

            public ScopedContravariantRegistrationSource(params Type[] types)
            {
                if (types == null)
                    throw new ArgumentNullException(nameof(types));
                if (!types.All(x => x.IsGenericTypeDefinition))
                    throw new ArgumentException("Supplied types should be generic type definitions");
                _types.AddRange(types);
            }

            public IEnumerable<IComponentRegistration> RegistrationsFor(Service service, Func<Service, IEnumerable<ServiceRegistration>> registrationAccessor)
            {
                var components = _source.RegistrationsFor(service, registrationAccessor);
                foreach (var c in components)
                {
                    var defs = c.Target.Services
                        .OfType<TypedService>()
                        .Select(x => x.ServiceType.GetGenericTypeDefinition());

                    if (defs.Any(_types.Contains))
                        yield return c;
                }
            }

            public bool IsAdapterForIndividualComponents => _source.IsAdapterForIndividualComponents;
        }

        private class AllConstructorFinder : IConstructorFinder
        {
            private static readonly ConcurrentDictionary<Type, ConstructorInfo[]> Cache =
                new ConcurrentDictionary<Type, ConstructorInfo[]>();

            public ConstructorInfo[] FindConstructors(Type targetType)
            {
                var result = Cache.GetOrAdd(targetType,
                    t => t.GetTypeInfo().DeclaredConstructors.ToArray());

                return result.Length > 0 ? result : throw new NoConstructorsFoundException(targetType);
            }
        }
    }
}
