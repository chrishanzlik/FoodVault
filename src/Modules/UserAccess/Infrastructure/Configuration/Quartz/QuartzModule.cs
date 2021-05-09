using Autofac;
using Quartz;

namespace FoodVault.Modules.UserAccess.Infrastructure.Configuration.Quartz
{
    /// <summary>
    /// IoC module for quartz.
    /// </summary>
    internal class QuartzModule : Module
    {
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(this.ThisAssembly)
                .Where(x => typeof(IJob)
                    .IsAssignableFrom(x))
                .InstancePerDependency();
        }
    }
}
