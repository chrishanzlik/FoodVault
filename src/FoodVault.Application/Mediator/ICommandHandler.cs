using MediatR;

namespace FoodVault.Application.Mediator
{
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, ICommandResult>
        where TCommand : ICommand
    {
    }
}
