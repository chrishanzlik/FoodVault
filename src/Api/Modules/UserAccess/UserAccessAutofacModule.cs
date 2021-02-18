using Autofac;
using FoodVault.Modules.UserAccess.Application.Contracts;
using FoodVault.Modules.UserAccess.Infrastructure;

namespace FoodVault.Api.Modules.UserAccess
{
    internal class UserAccessAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserAccessModule>()
                .As<IUserAccessModule>()
                .InstancePerLifetimeScope();
        }
    }
}
