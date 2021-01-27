using MediatR;

namespace FoodVault.Application.Commands
{
    /// <summary>
    /// Interface that defines a application command handler.
    /// </summary>
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, ICommandResult>
        where TCommand : ICommand
    {
    }
}
