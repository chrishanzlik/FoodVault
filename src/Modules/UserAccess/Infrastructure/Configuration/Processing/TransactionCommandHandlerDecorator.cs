using FoodVault.Framework.Application.Commands;
using FoodVault.Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Modules.UserAccess.Infrastructure.Configuration.Processing
{
    /// <summary>
    /// Command handler decorator for commiting transactions and dispatching domain events.
    /// </summary>
    /// <typeparam name="TCommand">Command type.</typeparam>
    internal class TransactionCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        private readonly ICommandHandler<TCommand> _decorated;

        private readonly IUnitOfWork _unitOfWork;

        private readonly UserAccessContext _userAccessContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionCommandHandlerDecorator" /> class.
        /// </summary>
        /// <param name="decorated">Decorated command handler.</param>
        /// <param name="unitOfWork">Unit of work transaction scope.</param>
        /// <param name="userAccessContext">User access DB context.</param>
        public TransactionCommandHandlerDecorator(
            ICommandHandler<TCommand> decorated,
            IUnitOfWork unitOfWork,
            UserAccessContext userAccessContext)
        {
            _decorated = decorated;
            _unitOfWork = unitOfWork;
            _userAccessContext = userAccessContext;
        }

        /// <inheritdoc />
        public async Task<ICommandResult> Handle(TCommand command, CancellationToken cancellationToken)
        {
            var result = await _decorated.Handle(command, cancellationToken);

            if (command is InternalCommand ic)
            {
                var internalCommand = await _userAccessContext.InternalCommands.FirstOrDefaultAsync(x => x.Id == ic.Id, cancellationToken);

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
