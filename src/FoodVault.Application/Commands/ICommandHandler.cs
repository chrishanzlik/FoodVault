using MediatR;

namespace FoodVault.Application.Commands
{
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, ICommandResult>
        where TCommand : ICommand
    {
    }
}
