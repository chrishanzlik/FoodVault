using Autofac;
using FoodVault.Application.Mediator;
using MediatR;
using System.Threading.Tasks;

namespace FoodVault.Infrastructure.Storage.Work
{
    internal class IsolatedCommandExecutor : ICommandExecutor
    {
        private readonly IContainer _container;

        public IsolatedCommandExecutor(IContainer container)
        {
            _container = container;
        }

        public async Task<ICommandResult> Execute(ICommand command)
        {
            using var scope = _container.BeginLifetimeScope();

            var mediator = scope.Resolve<IMediator>();

            return await mediator.Send(command);
        }
    }
}
