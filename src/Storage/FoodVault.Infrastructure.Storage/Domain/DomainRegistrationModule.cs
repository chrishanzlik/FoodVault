using Autofac;
using FoodVault.Application.Storage.FoodStorages.DomainServices;
using FoodVault.Domain.Storage.FoodStorages;

namespace FoodVault.Infrastructure.Storage.Domain
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
