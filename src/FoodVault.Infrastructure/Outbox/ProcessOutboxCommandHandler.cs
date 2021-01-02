using Dapper;
using FoodVault.Application.Database;
using FoodVault.Application.Events;
using FoodVault.Application.Mediator;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Infrastructure.Outbox
{
    /// <summary>
    /// Command handler for processing the outbox.
    /// </summary>
    public class ProcessOutboxCommandHandler : ICommandHandler<ProcessOutboxCommand>
    {
        private readonly Assembly _commandsAssembly;
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessOutboxCommandHandler" /> class.
        /// </summary>
        /// <param name="commandsAssembly">Assembly containing application commands.</param>
        /// <param name="dbConnectionFactory">Database connection factory.</param>
        /// <param name="mediator">Application mediator.</param>
        public ProcessOutboxCommandHandler(
            Assembly commandsAssembly,
            IDbConnectionFactory dbConnectionFactory,
            IMediator mediator)
        {
            _commandsAssembly = commandsAssembly;
            _dbConnectionFactory = dbConnectionFactory;
            _mediator = mediator;
        }

        /// <inheritdoc />
        public async Task<ICommandResult> Handle(ProcessOutboxCommand request, CancellationToken cancellationToken)
        {
            const string sql =
                "SELECT " +
                "[OutboxMessage].[Id], " +
                "[OutboxMessage].[EventType], " +
                "[OutboxMessage].[Payload] " +
                "FROM [dbo].[OutboxMessages] AS [OutboxMessage] " +
                "WHERE [OutboxMessage].[ProcessedDate] IS NULL";

            const string processSql =
                "UPDATE [dbo].[OutboxMessages] " +
                "SET [ProcessedDate] = @date " +
                "WHERE [Id] = @id";

            var con = _dbConnectionFactory.GetOpen();

            var pendingMessages = (await con.QueryAsync<OutboxMessageDto>(sql)).ToList();
            if (pendingMessages.Any())
            {
                foreach(var msg in pendingMessages)
                {
                    var t = _commandsAssembly.GetType(msg.EventType);
                    var ev = JsonConvert.DeserializeObject(msg.Payload, t) as IDomainEventNotification;

                    await _mediator.Publish(ev, cancellationToken);
                    await con.ExecuteAsync(processSql, new { date = DateTime.UtcNow, id =  msg.Id });
                }
            }

            return CommandResult.Ok();
        }
    }
}
