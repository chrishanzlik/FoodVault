using FluentValidation;
using FoodVault.Framework.Application.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Infrastructure.Configuration.Processing
{
    /// <summary>
    /// Command handler decorator for command validation purposes.
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public class ValidationCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        private readonly ICommandHandler<TCommand> _decorated;
        private readonly IList<IValidator<TCommand>> _validators;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionCommandHandlerDecorator" /> class.
        /// </summary>
        /// <param name="decorated">Decorated command handler.</param>
        /// <param name="validators">Applicable validators for the given command type.</param>
        public ValidationCommandHandlerDecorator(
            ICommandHandler<TCommand> decorated,
            IList<IValidator<TCommand>> validators)
        {
            _decorated = decorated;
            _validators = validators;
        }

        /// <inheritdoc />
        public Task<ICommandResult> Handle(TCommand command, CancellationToken cancellationToken)
        {
            var errors = _validators
                .Select(validator => validator.Validate(command))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .Select(error => error.ErrorMessage)
                .ToList();

            if (errors.Any())
            {
                return Task.FromResult(CommandResult.BadParameters(errors));
            }

            return _decorated.Handle(command, cancellationToken);
        }
    }
}
