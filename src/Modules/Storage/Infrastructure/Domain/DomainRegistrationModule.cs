using Autofac;
using FoodVault.Modules.Storage.Application.FoodStorages.DomainServices;
using FoodVault.Modules.Storage.Domain.FoodStorages;

namespace FoodVault.Modules.Storage.Infrastructure.Domain
{
    /// <summary>
    /// IoC container registrations for 'Domain' stuff.
    /// </summary>
    public class DomainRegistrationModule : Module
    {
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProdcutExistsSqlChecker>()
                .As<IProductExistsChecker>()
                .InstancePerLifetimeScope();
        }
    }
}
