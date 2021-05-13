using Autofac;
using FoodVault.Modules.Storage.Application.FoodStorages;

namespace FoodVault.Modules.Storage.Infrastructure.Configuration.Application
{
    /// <summary>
    /// IoC container registrations for 'Application' stuff.
    /// </summary>
    internal class ApplicationModule : Module
    {
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StoragePermissionChecker>()
                .As<IStoragePermissionChecker>()
                .InstancePerLifetimeScope();
        }
    }
}
