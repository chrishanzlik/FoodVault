using Dapper;
using FoodVault.Framework.Application.Commands;
using FoodVault.Framework.Application.DataAccess;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Modules.UserAccess.Infrastructure.Configuration.Processing.Inbox
{
    /// <summary>
    /// Command handler for the <see cref="ProcessInboxCommand"/>.
    /// </summary>
    public class ProcessInboxCommandHandler : ICommandHandler<ProcessInboxCommand>
    {
        private readonly IMediator _mediator;
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessInboxCommandHandler" /> class.
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="dbConnectionFactory"></param>
        public ProcessInboxCommandHandler(
            IMediator mediator,
            IDbConnectionFactory dbConnectionFactory)
        {
            _mediator = mediator;
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <inheritdoc />
        public async Task<ICommandResult> Handle(ProcessInboxCommand request, CancellationToken cancellationToken)
        {
            const string fetchSql =
                "SELECT " +
                "[InboxMessage].[Id], " +
                "[InboxMessage].[EventType]," +
                "[InboxMessage].[Payload] " +
                "FROM [users].[InboxMessages] AS [InboxMessage] " +
                "WHERE [InboxMessage].[ProcessedDate] IS NULL " +
                "ORDER BY [InboxMessage].[RaisingTime]";

            const string markAsCompleteSql =
                "UPDATE [users].[InboxMessages] " +
                "SET [ProcessedDate] = @Date " +
                "WHERE [Id] = @Id";

            var connection = _dbConnectionFactory.GetOpen();
            var pendingMessages = await connection.QueryAsync<InboxMessageDto>(fetchSql);
            foreach(var message in pendingMessages)
            {
                var assembly = AppDomain.CurrentDomain
                    .GetAssemblies()
                    .SingleOrDefault(asm => message.EventType.Contains(asm.GetName().Name));

                var type = assembly.GetType(message.EventType);

                var notification = JsonConvert.DeserializeObject(message.Payload, type);

                await _mediator.Publish((INotification)notification, cancellationToken);

                await connection.ExecuteScalarAsync(markAsCompleteSql, new
                {
                    Date = DateTime.UtcNow,
                    Id = message.Id
                });
            }

            return CommandResult.Ok();
        }
    }
}
