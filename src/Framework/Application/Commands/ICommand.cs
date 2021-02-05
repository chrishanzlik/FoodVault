using MediatR;
using System;

namespace FoodVault.Framework.Application.Commands
{
    /// <summary>
    /// Interface that defines a application command.
    /// </summary>
    public interface ICommand : IRequest<ICommandResult>
    {
        /// <summary>
        /// Gets the command identifier.
        /// </summary>
        Guid Id { get; }
    }
}
