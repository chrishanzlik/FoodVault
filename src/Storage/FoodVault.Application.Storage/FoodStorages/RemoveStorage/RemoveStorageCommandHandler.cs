using FoodVault.Core.Mediator;
using FoodVault.Domain.Storage.FoodStorages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Application.Storage.FoodStorages.RemoveStorage
{
    /// <summary>
    /// Command handler for the <see cref="RemoveStorageCommand"/>.
    /// </summary>
    public class RemoveStorageCommandHandler : ICommandHandler<RemoveStorageCommand>
    {
        private readonly IFoodStorageRepository _foodStorageRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateStorageCommandHandler" /> class.
        /// </summary>
        /// <param name="foodStorageRepository">FoodStorage repository.</param>
        public RemoveStorageCommandHandler(IFoodStorageRepository foodStorageRepository)
        {
            _foodStorageRepository = foodStorageRepository;
        }

        /// <inheritdoc />
        public async Task<ICommandResult> Handle(RemoveStorageCommand request, CancellationToken cancellationToken)
        {
            FoodStorageId id = new FoodStorageId(request.FoodStorageId);

            await _foodStorageRepository.RemoveAsync(id);

            //TODO:
            return new CommandResult();
        }
    }
}
