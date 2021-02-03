using Autofac;
using Dapper;
using FoodVault.Framework.Application.Database;
using FoodVault.Framework.Infrastructure.EventBus;
using FoodVault.Framework.Infrastructure.Serialization;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Infrastructure.Configuration.EventBus
{
    internal class IntegrationEventGenericHandler<T> : IIntegrationEventHandler<T>
        where T : IntegrationEvent
    {
        public async Task Handle(T @event)
        {
            using var scope = StorageCompositionRoot.BeginLifetimeScope();
            using var connection = scope.Resolve<IDbConnectionFactory>().GetOpen();

            string eventType = @event.GetType().FullName;
            var payload = JsonConvert.SerializeObject(@event, new JsonSerializerSettings
            {
                ContractResolver = new AllPropertiesContractResolver()
            });

            const string sql = "INSERT INTO [storage].[InboxMessages] (Id, OccurredOn, EventType, Payload) " +
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
