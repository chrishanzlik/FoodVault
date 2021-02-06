using Autofac;
using Dapper;
using FoodVault.Framework.Application.DataAccess;
using FoodVault.Framework.Infrastructure.EventBus;
using FoodVault.Framework.Infrastructure.Serialization;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Infrastructure.Configuration.EventBus
{
    /// <summary>
    /// Generic integration event handler.
    /// </summary>
    /// <typeparam name="T">Type of the integration event.</typeparam>
    internal class IntegrationEventGenericHandler<T> : IIntegrationEventHandler<T>
        where T : IntegrationEvent
    {
        /// <inheritdoc />
        public async Task Handle(T @event)
        {
            using var scope = StorageCompositionRoot.BeginLifetimeScope();
            using var connection = scope.Resolve<IDbConnectionFactory>().GetOpen();

            string eventType = @event.GetType().FullName;
            var payload = JsonConvert.SerializeObject(@event, new JsonSerializerSettings
            {
                ContractResolver = new AllPropertiesContractResolver()
            });

            const string sql = "INSERT INTO [storage].[InboxMessages] (Id, RaisingTime, EventType, Payload) " +
                      "VALUES (@Id, @OccurredOn, @EventType, @Payload)";

            await connection.ExecuteScalarAsync(sql, new
            {
                @event.Id,
                @event.OccurredOn,
                eventType,
                payload
            });
        }
    }
}
