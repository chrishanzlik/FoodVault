using Autofac;
using FoodVault.Modules.Storage.Application.FoodStorages;
using FoodVault.Modules.Storage.Application.FoodStorages.CreateStorage;
using FoodVault.Modules.Storage.Domain.FoodStorages;
using FoodVault.Modules.Storage.Domain.Users;

namespace FoodVault.Modules.Storage.Infrastructure.Domain
{
    /// <summary>
    /// IoC container registrations for 'Domain' stuff.
    /// </summary>
    internal class DomainModule : Module
    {
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StorageNameUniquessSqlChecker>()
                .As<IStorageNameUniquessChecker>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UserContext>()
                .As<IUserContext>()
                .InstancePerLifetimeScope();
        }
    }
}
