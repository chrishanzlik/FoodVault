using Dapper;
using FoodVault.Application.Database;
using FoodVault.Application.Events;
using FoodVault.Core.Mediator;
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
        private readonly Assembly _commandAssembly;
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessOutboxCommandHandler" /> class.
        /// </summary>
        /// <param name="commandAssembly">Assembly containing application commands.</param>
        /// <param name="dbConnectionFactory">Database connection factory.</param>
        /// <param name="mediator">Application mediator.</param>
        public ProcessOutboxCommandHandler(
            Assembly commandAssembly,
            IDbConnectionFactory dbConnectionFactory,
            IMediator mediator)
        {
            _commandAssembly = commandAssembly;
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
                "WHERE [OutboxMessage].[RaisingTime] IS NULL";

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
                    var t = _commandAssembly.GetType(msg.EventType);
                    var ev = JsonConvert.DeserializeObject(msg.Payload, t) as IDomainEventNotification;

                    await _mediator.Publish(ev, cancellationToken);
                    await con.ExecuteAsync(processSql, new { date = DateTime.UtcNow, id =  msg.Id });
                }
            }

            return CommandResult.Ok();
        }
    }
}
