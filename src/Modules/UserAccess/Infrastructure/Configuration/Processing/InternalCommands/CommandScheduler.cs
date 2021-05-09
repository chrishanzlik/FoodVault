using Dapper;
using FoodVault.Framework.Application;
using FoodVault.Framework.Application.Commands;
using FoodVault.Framework.Application.DataAccess;
using FoodVault.Framework.Infrastructure.Serialization;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace FoodVault.Modules.UserAccess.Infrastructure.Configuration.Processing.InternalCommands
{
    /// <summary>
    /// Schedules commands as SQL statement for later execution.
    /// </summary>
    internal class CommandScheduler : ICommandScheduler
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandScheduler" /> class.
        /// </summary>
        /// <param name="dbConnectionFactory">Sql connection factory.</param>
        public CommandScheduler(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <inheritdoc />
        public async Task ScheduleAsync(ICommand command)
        {
            const string sql =
                "INSERT INTO [users].[InternalCommands] ([Id], [EnqueueDate] , [CommandType], [Payload]) VALUES " +
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
