using Autofac;
using FoodVault.Framework.Application.DataAccess;
using FoodVault.Framework.Infrastructure.DataAccess;
using FoodVault.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FoodVault.Modules.UserAccess.Infrastructure.Configuration.DataAccess
{
    /// <summary>
    /// IoC container registrations for 'DataAccess' stuff.
    /// </summary>
    internal class DataAccessModule : Module
    {
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataAccessModule" /> class.
        /// </summary>
        /// <param name="connectionString">Connection string to connect with the <see cref="UserAccessContext"/>.</param>
        public DataAccessModule(string connectionString)
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
                    var options = new DbContextOptionsBuilder<UserAccessContext>()
                        .UseSqlServer(_connectionString, options =>
                        {
                            options.MigrationsHistoryTable("__MigrationHistory", "users");
                        })
                        .ReplaceService<IValueConverterSelector, EntityIdValueConverterSelector>()
                        .Options;

                    return new UserAccessContext(options);
                })
                .AsSelf()
                .As<DbContext>()
                .InstancePerLifetimeScope();
        }
    }
}
