using Autofac;
using FoodVault.Modules.Storage.Application.Contracts;
using FoodVault.Modules.Storage.Infrastructure;

namespace FoodVault.Api.Modules.Storages
{
    /// <summary>
    /// Autofac registrations for the <see cref="StorageModule"/>.
    /// </summary>
    internal class StorageAutofacModule : Module
    {
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StorageModule>()
                .As<IStorageModule>()
                .InstancePerLifetimeScope();
        }
    }
}
