using Autofac;
using FoodVault.Framework.Application.Commands;
using FoodVault.Framework.Infrastructure;
using MediatR;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Infrastructure.Work
{
    internal class IsolatedCommandExecutor : ICommandExecutor
    {
        private readonly ILifetimeScope _lifetimeScope;

        public IsolatedCommandExecutor(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        public async Task<ICommandResult> Execute(ICommand command)
        {
            using var scope = _lifetimeScope.BeginLifetimeScope();

            var mediator = scope.Resolve<IMediator>();

            return await mediator.Send(command);
        }
    }
}
