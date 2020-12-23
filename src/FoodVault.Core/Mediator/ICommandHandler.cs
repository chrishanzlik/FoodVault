using MediatR;

namespace FoodVault.Core.Mediator
{
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, ICommandResult>
        where TCommand : ICommand
    {
    }
}
