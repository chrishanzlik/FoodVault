using FoodVault.Framework.Application;
using FoodVault.Framework.Application.Commands;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Infrastructure.Configuration.Processing
{
    public class LoggingCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        //private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly ICommandHandler<TCommand> _decorated;
        private readonly ILogger _logger;

        public LoggingCommandHandlerDecorator(
            ICommandHandler<TCommand> decorated,
            ILogger logger)
            //IExecutionContextAccessor executionContextAccessor)
        {
            _decorated = decorated;
            _logger = logger;
            //_executionContextAccessor = executionContextAccessor;
        }

        public async Task<ICommandResult> Handle(TCommand command, CancellationToken cancellationToken)
        {
            if (command is IRecurringCommand)
            {
                return await _decorated.Handle(command, cancellationToken);
            }

            var cmdName = command.GetType().Name;

            _logger.LogInformation($"Executing command: {cmdName}");

            var result = await _decorated.Handle(command, cancellationToken);

            if (result.Success)
                _logger.LogInformation($"{cmdName} was executed successful.");
            else
                _logger.LogInformation($"{cmdName} was executed not successful.");

            return result;
        }
    }
}
