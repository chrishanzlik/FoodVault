using FluentValidation;
using FoodVault.Application.Mediator;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Application.Validation
{
    /// <summary>
    /// Validates commands inside the mediator pipeline.
    /// </summary>
    /// <typeparam name="TCommand">Type of the command.</typeparam>
    public class CommandValidationPipelineBehavior<TCommand> : IPipelineBehavior<TCommand, ICommandResult>
    {
        private readonly IList<IValidator<TCommand>> _validators;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandValidationPipelineBehavior" /> class.
        /// </summary>
        /// <param name="validators">Matching validators for the given command.</param>
        public CommandValidationPipelineBehavior(IList<IValidator<TCommand>> validators)
        {
            _validators = validators;
        }

        /// <inheritdoc />
        public async Task<ICommandResult> Handle(TCommand request, CancellationToken cancellationToken, RequestHandlerDelegate<ICommandResult> next)
        {
            var errors = _validators
                .Select(validator => validator.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .Select(error => error.ErrorMessage)
                .ToList();

            if (errors.Any())
            {
                return CommandResult.BadParameters(errors);
            }

            return (await next());
        }
    }
}
