using Autofac;
using FoodVault.Infrastructure.Database;
using FoodVault.Modules.Storage.Domain.FoodStorages;
using FoodVault.Modules.Storage.Domain.Products;
using FoodVault.Modules.Storage.Infrastructure.Domain.FoodStorages;
using FoodVault.Modules.Storage.Infrastructure.Domain.Products;
using FoodVault.Framework.Application.DataAccess;
using FoodVault.Framework.Application.FileUploads;
using FoodVault.Framework.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using FoodVault.Modules.Storage.Infrastructure.FileUploads;

namespace FoodVault.Modules.Storage.Infrastructure.Configuration.DataAccess
{
    /// <summary>
    /// IoC container registrations for 'Database' stuff.
    /// </summary>
    internal class DatabaseModule : Module
    {
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseModule" /> class.
        /// </summary>
        /// <param name="connectionString">Connection string to connect with the <see cref="StorageContext"/>.</param>
        public DatabaseModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SqlConnectionFactory>()
               .As<IDbConnectionFactory>()
               .WithParameter("connectionString", _connectionString)
               .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(type => type.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .FindConstructorsWith(new ConstructorFinder());

            builder
                .Register(c =>
                {
                    var options = new DbContextOptionsBuilder<StorageContext>()
                        .UseSqlServer(_connectionString)
                        .ReplaceService<IValueConverterSelector, EntityIdValueConverterSelector>()
                        .Options;

                    return new StorageContext(options);
                })
                .AsSelf()
                .As<DbContext>()
                .InstancePerLifetimeScope();
        }
    }
}
