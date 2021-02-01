using MediatR;

namespace FoodVault.Framework.Application.Commands
{
    /// <summary>
    /// Interface that defines a application command.
    /// </summary>
    public interface ICommand : IRequest<ICommandResult>
    {
    }
}
