using Autofac;
using FoodVault.Framework.Application.FileUploads;
using FoodVault.Modules.Storage.Infrastructure.FileUploads;

namespace FoodVault.Modules.Storage.Infrastructure.Configuration.FileUploads
{
    /// <summary>
    /// IoC module for file uploads.
    /// </summary>
    internal class FileUploadModule : Module
    {
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LocalDiskFileStorage>()
                .As<IFileStorage>()
                .InstancePerLifetimeScope();
        }
    }
}
