using FoodVault.Application.Commands;
using FoodVault.Domain;
using FoodVault.Infrastructure.Storage.Database;
using Microsoft.EntityFrameworkCore;
using System;
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

        private readonly StorageContext _storageContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionCommandHandlerDecorator" /> class.
        /// </summary>
        /// <param name="decorated">Decorated command handler.</param>
        /// <param name="unitOfWork">Unit of work transaction scope.</param>
        public TransactionCommandHandlerDecorator(
            ICommandHandler<TCommand> decorated,
            IUnitOfWork unitOfWork,
            StorageContext storageContext)
        {
            _decorated = decorated;
            _unitOfWork = unitOfWork;
            _storageContext = storageContext;
        }

        /// <inheritdoc />
        public async Task<ICommandResult> Handle(TCommand command, CancellationToken cancellationToken)
        {
            var result = await _decorated.Handle(command, cancellationToken);

            if (command is InternalCommandBase ic)
            {
                var internalCommand = await _storageContext.InternalCommands.FirstOrDefaultAsync(x => x.Id == ic.Id, cancellationToken);

                if (internalCommand != null)
                {
                    internalCommand.ProcessedDate = DateTime.UtcNow;
                }
            }

            await _unitOfWork.CommitAsync(cancellationToken);

            return result;
        }
    }
}
