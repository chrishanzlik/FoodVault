using Dapper;
using FoodVault.Framework.Application.Commands;
using FoodVault.Framework.Application.DataAccess;
using FoodVault.Framework.Application.Events;
using FoodVault.Framework.Infrastructure.DomainEvents;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Modules.UserAccess.Infrastructure.Configuration.Processing.Outbox
{
    /// <summary>
    /// Command handler for processing the outbox.
    /// </summary>
    internal class ProcessOutboxCommandHandler : ICommandHandler<ProcessOutboxCommand>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly IMediator _mediator;
        private readonly IDomainNotificationsRegistry _domainNotificationRegistry;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessOutboxCommandHandler" /> class.
        /// </summary>
        /// <param name="domainNotificationsRegistry">Name-Type mappings of notifications.</param>
        /// <param name="dbConnectionFactory">Database connection factory.</param>
        /// <param name="mediator">Application mediator.</param>
        public ProcessOutboxCommandHandler(
            IDbConnectionFactory dbConnectionFactory,
            IMediator mediator,
            IDomainNotificationsRegistry domainNotificationsRegistry)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _mediator = mediator;
            _domainNotificationRegistry = domainNotificationsRegistry;
        }

        /// <inheritdoc />
        public async Task<ICommandResult> Handle(ProcessOutboxCommand request, CancellationToken cancellationToken)
        {
            const string sql =
                "SELECT " +
                "[OutboxMessage].[Id], " +
                "[OutboxMessage].[EventType], " +
                "[OutboxMessage].[Payload] " +
                "FROM [users].[OutboxMessages] AS [OutboxMessage] " +
                "WHERE [OutboxMessage].[ProcessedDate] IS NULL";

            const string processSql =
                "UPDATE [users].[OutboxMessages] " +
                "SET [ProcessedDate] = @date " +
                "WHERE [Id] = @id";

            var con = _dbConnectionFactory.GetOpen();

            var pendingMessages = (await con.QueryAsync<OutboxMessageDto>(sql)).ToList();
            if (pendingMessages.Any())
            {
                foreach(var msg in pendingMessages)
                {
                    var t = _domainNotificationRegistry.GetType(msg.EventType);
                    var ev = JsonConvert.DeserializeObject(msg.Payload, t) as IDomainEventNotification;

                    await _mediator.Publish(ev, cancellationToken);
                    await con.ExecuteAsync(processSql, new { date = DateTime.UtcNow, id =  msg.Id });
                }
            }

            return CommandResult.Ok();
        }
    }
}
