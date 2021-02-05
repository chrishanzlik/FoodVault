using Autofac;
using FoodVault.Framework.Application.FileUploads;
using FoodVault.Modules.Storage.Infrastructure.FileUploads;

namespace FoodVault.Modules.Storage.Infrastructure.Configuration.FileUploads
{
    internal class FileUploadModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LocalDiskFileStorage>()
                .As<IFileStorage>()
                .InstancePerLifetimeScope();
        }
    }
}
