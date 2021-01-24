using Autofac;
using FoodVault.Application.Commands;
using MediatR;
using System.Threading.Tasks;

namespace FoodVault.Infrastructure.Storage.Work
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
