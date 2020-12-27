using Autofac;
using FoodVault.Application;
using FoodVault.Domain;
using FoodVault.Domain.Storage.FoodStorages;
using FoodVault.Domain.Storage.Products;
using FoodVault.Infrastructure.Database;
using FoodVault.Infrastructure.Domain;
using FoodVault.Infrastructure.Storage.Domain.FoodStorages;
using FoodVault.Infrastructure.Storage.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FoodVault.Infrastructure.Storage.Database
{
    /// <summary>
    /// IoC container registrations for 'Database' stuff.
    /// </summary>
    public class DatabaseRegistrationModule : Module
    {
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseRegistrationModule" /> class.
        /// </summary>
        /// <param name="connectionString">Connection string to connect with the <see cref="StorageContext"/>.</param>
        public DatabaseRegistrationModule(string connectionString)
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

            builder.RegisterType<UnitOfWork<StorageContext>>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<FoodStorageRepository>()
                .As<IFoodStorageRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ProductRepository>()
                .As<IProductRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<EntityIdValueConverterSelector>()
                .As<IValueConverterSelector>()
                .InstancePerLifetimeScope();

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
