using FoodVault.Modules.Storage.Infrastructure.Database;
using FoodVault.Framework.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FoodVault.Modules.Storage.Infrastructure.Work
{
    /// <summary>
    /// Dispatches stored internal commands.
    /// </summary>
    internal class CommandDispatcher : ICommandDispatcher
    {
        private readonly IMediator _mediator;
        private readonly StorageContext _storageContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandDispatcher" /> class.
        /// </summary>
        /// <param name="mediator">Mediator.</param>
        /// <param name="storageContext">Database context.</param>
        public CommandDispatcher(IMediator mediator, StorageContext storageContext)
        {
            _mediator = mediator;
            _storageContext = storageContext;
        }

        /// <inheritdoc />
        public async Task DispatchCommandAsync(Guid id)
        {
            var internalCommand = await _storageContext.InternalCommands.SingleOrDefaultAsync(x => x.Id == id);

            var t = Assemblies.Application.GetType(internalCommand.CommandType);
            var command = JsonConvert.DeserializeObject(internalCommand.Payload, t) as ICommand;

            internalCommand.ProcessedDate = DateTime.UtcNow;

            await _mediator.Send(command);
        }
    }
}
