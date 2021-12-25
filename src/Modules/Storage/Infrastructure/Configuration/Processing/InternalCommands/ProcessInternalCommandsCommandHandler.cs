using Dapper;
using FoodVault.Framework.Application.Commands;
using FoodVault.Framework.Application.DataAccess;
using Newtonsoft.Json;
using Polly;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Infrastructure.Configuration.Processing.InternalCommands
{
    /// <summary>
    /// Command handler for the <see cref="ProcessInternalCommandsCommand"/>.
    /// </summary>
    internal class ProcessInternalCommandsCommandHandler : ICommandHandler<ProcessInternalCommandsCommand>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessInternalCommandsCommandHandler" /> class.
        /// </summary>
        /// <param name="dbConnectionFactory">Db connection factory.</param>
        public ProcessInternalCommandsCommandHandler(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <inheritdoc />
        public async Task<ICommandResult> Handle(ProcessInternalCommandsCommand request, CancellationToken cancellationToken)
        {
            var connection = _dbConnectionFactory.GetOpen();

            const string fetchSql =
                "SELECT " +
                "[Command].[Id], " +
                "[Command].[CommandType], " +
                "[Command].[Payload] " +
                "FROM [storage].[InternalCommands] AS [Command] " +
                "WHERE [Command].[ProcessedDate] IS NULL " +
                "ORDER BY [Command].[EnqueueDate]";

            const string errorSql =
                "UPDATE [storage].[InternalCommands] " +
                "SET [ProcessedDate] = @processed, [Error] = @error " +
                "WHERE [Id] = @id";

            var pendingCommands = (await connection.QueryAsync<InternalCommandDto>(fetchSql)).AsList();
            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(2),
                    TimeSpan.FromSeconds(3)
                });

            foreach (var internalCommand in pendingCommands)
            {
                var policyResult = await policy.ExecuteAndCaptureAsync(() => ExecuteCommandAsync(internalCommand));

                if (policyResult.Outcome == OutcomeType.Failure)
                {
                    await connection.ExecuteScalarAsync(errorSql, new
                    {
                        processed = DateTime.UtcNow,
                        error = policyResult.FinalException.ToString(),
                        id = internalCommand.Id
                    });
                }
            }

            return CommandResult.Ok();
        }

        private async Task<ICommandResult> ExecuteCommandAsync(InternalCommandDto internalCommand)
        {
            var t = Assemblies.Application.GetType(internalCommand.CommandType);
            var command = JsonConvert.DeserializeObject(internalCommand.Payload, t) as ICommand;
            return await CommandExecutor.ExecuteAsync(command);
        }
    }
}
