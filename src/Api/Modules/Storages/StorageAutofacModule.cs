using Autofac;
using FoodVault.Modules.Storage.Application.Contracts;
using FoodVault.Modules.Storage.Infrastructure;

namespace FoodVault.Api.Modules.Storages
{
    internal class StorageAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StorageModule>()
                .As<IStorageModule>()
                .InstancePerLifetimeScope();
        }
    }
}
