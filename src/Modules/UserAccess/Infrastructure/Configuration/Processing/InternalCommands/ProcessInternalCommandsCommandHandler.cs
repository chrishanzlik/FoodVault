using Dapper;
using FoodVault.Framework.Application.Commands;
using FoodVault.Framework.Application.DataAccess;
using FoodVault.Framework.Infrastructure.DomainEvents;
using Newtonsoft.Json;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Modules.UserAccess.Infrastructure.Configuration.Processing.InternalCommands
{
    /// <summary>
    /// Command handler for the <see cref="ProcessInternalCommandsCommand"/>.
    /// </summary>
    internal class ProcessInternalCommandsCommandHandler : ICommandHandler<ProcessInternalCommandsCommand>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly IDomainNotificationsRegistry _domainNotificationsRegistry;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessInternalCommandsCommandHandler" /> class.
        /// </summary>
        /// <param name="domainNotificationsRegistry">Contains Name-Type maps of notifications.</param>
        /// <param name="dbConnectionFactory">Db connection factory.</param>
        public ProcessInternalCommandsCommandHandler(
            IDbConnectionFactory dbConnectionFactory,
            IDomainNotificationsRegistry domainNotificationsRegistry)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _domainNotificationsRegistry = domainNotificationsRegistry;
        }

        /// <inheritdoc />
        public async Task<ICommandResult> Handle(ProcessInternalCommandsCommand request, CancellationToken cancellationToken)
        {
            var connection = _dbConnectionFactory.GetOpen();

            const string sql = "SELECT " +
                               "[Command].[CommandType], " +
                               "[Command].[Payload] " +
                               "FROM [users].[InternalCommands] AS [Command] " +
                               "WHERE [Command].[ProcessedDate] IS NULL";

            var pendingCommands = (await connection.QueryAsync<InternalCommandDto>(sql)).ToList();

            foreach (var internalCommand in pendingCommands)
            {
                var t = _domainNotificationsRegistry.GetType(internalCommand.CommandType);
                var command = JsonConvert.DeserializeObject(internalCommand.Payload, t) as ICommand;

                //TODO: handle result
                await CommandExecutor.ExecuteAsync(command);
            }

            return CommandResult.Ok();
        }
    }
}
