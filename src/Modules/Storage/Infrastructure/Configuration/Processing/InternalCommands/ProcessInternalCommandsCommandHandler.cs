using Dapper;
using FoodVault.Framework.Application.DataAccess;
using FoodVault.Framework.Application.Commands;
using Newtonsoft.Json;
using System.Linq;
using System.Reflection;
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
        private readonly Assembly _commandsAssembly;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessInternalCommandsCommandHandler" /> class.
        /// </summary>
        /// <param name="commandsAssembly">Assembly where all application commands are registered.</param>
        /// <param name="dbConnectionFactory">Db connection factory.</param>
        public ProcessInternalCommandsCommandHandler(
            Assembly commandsAssembly,
            IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _commandsAssembly = commandsAssembly;
        }

        /// <inheritdoc />
        public async Task<ICommandResult> Handle(ProcessInternalCommandsCommand request, CancellationToken cancellationToken)
        {
            var connection = _dbConnectionFactory.GetOpen();

            const string sql = "SELECT " +
                               "[Command].[CommandType], " +
                               "[Command].[Payload] " +
                               "FROM [storage].[InternalCommands] AS [Command] " +
                               "WHERE [Command].[ProcessedDate] IS NULL";

            var pendingCommands = (await connection.QueryAsync<InternalCommandDto>(sql)).ToList();

            foreach (var internalCommand in pendingCommands)
            {
                var t = _commandsAssembly.GetType(internalCommand.CommandType);
                var command = JsonConvert.DeserializeObject(internalCommand.Payload, t) as ICommand;

                //TODO: handle result
                await CommandExecutor.ExecuteAsync(command);
            }

            return CommandResult.Ok();
        }
    }
}
