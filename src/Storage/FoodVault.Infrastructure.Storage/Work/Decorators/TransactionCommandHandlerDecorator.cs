using FoodVault.Application.Mediator;
using FoodVault.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Infrastructure.Storage.Work.Decorators
{
    /// <summary>
    /// Command handler decorator for commiting transactions and dispatching domain events.
    /// </summary>
    /// <typeparam name="TCommand">Command type.</typeparam>
    public class TransactionCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        private readonly ICommandHandler<TCommand> _decorated;

        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionCommandHandlerDecorator" /> class.
        /// </summary>
        /// <param name="decorated">Decorated command handler.</param>
        /// <param name="unitOfWork">Unit of work transaction scope.</param>
        public TransactionCommandHandlerDecorator(
            ICommandHandler<TCommand> decorated,
            IUnitOfWork unitOfWork)
        {
            _decorated = decorated;
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public async Task<ICommandResult> Handle(TCommand request, CancellationToken cancellationToken)
        {
            var result = await _decorated.Handle(request, cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);

            return result;
        }
    }
}
