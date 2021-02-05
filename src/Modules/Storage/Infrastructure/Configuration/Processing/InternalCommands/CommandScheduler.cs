using Dapper;
using FoodVault.Framework.Application;
using FoodVault.Framework.Application.Commands;
using FoodVault.Framework.Application.DataAccess;
using FoodVault.Framework.Infrastructure.Serialization;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Infrastructure.Configuration.Processing.InternalCommands
{
    public class CommandScheduler : ICommandScheduler
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public CommandScheduler(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task ScheduleAsync<T>(ICommand command)
        {
            const string sql =
                "INSERT INTO [storage].[InternalCommands] ([Id], [EnqueueDate] , [CommandType], [Payload]) VALUES " +
                "(@Id, @EnqueueDate, @CommandType, @Payload)";

            var connection = _dbConnectionFactory.GetOpen();
            var payload = JsonConvert.SerializeObject(command, new JsonSerializerSettings
            {
                ContractResolver = new AllPropertiesContractResolver()
            });

            await connection.ExecuteAsync(sql, new
            {
                Id = command.Id,
                EnqueueDate = DateTime.UtcNow,
                CommandType = command.GetType().FullName,
                Payload = payload
            });
        }
    }
}
