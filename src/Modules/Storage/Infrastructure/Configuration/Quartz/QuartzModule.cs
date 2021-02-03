using Autofac;
using Quartz;

namespace FoodVault.Modules.Storage.Infrastructure.Configuration.Quartz
{
    internal class QuartzModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(this.ThisAssembly)
                .Where(x => typeof(IJob)
                    .IsAssignableFrom(x))
                .InstancePerDependency();
        }
    }
}
