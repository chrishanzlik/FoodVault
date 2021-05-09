using Autofac;
using FoodVault.Modules.UserAccess.Application.Contracts;
using FoodVault.Modules.UserAccess.Infrastructure;

namespace FoodVault.Api.Modules.UserAccess
{
    /// <summary>
    /// Autofac registrations for the <see cref="UserAccessModule"/>.
    /// </summary>
    internal class UserAccessAutofacModule : Module
    {
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserAccessModule>()
                .As<IUserAccessModule>()
                .InstancePerLifetimeScope();
        }
    }
}
