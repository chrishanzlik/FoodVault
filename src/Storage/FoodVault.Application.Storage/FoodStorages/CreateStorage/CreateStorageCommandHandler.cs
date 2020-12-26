using FoodVault.Core.Mediator;
using FoodVault.Domain.Storage.FoodStorages;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Application.Storage.FoodStorages.CreateStorage
{
    /// <summary>
    /// Command handler for the <see cref="CreateStorageCommand"/>.
    /// </summary>
    public class CreateStorageCommandHandler : ICommandHandler<CreateStorageCommand>
    {
        private readonly IFoodStorageRepository _foodStorageRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateStorageCommandHandler" /> class.
        /// </summary>
        /// <param name="foodStorageRepository">FoodStorage repository.</param>
        public CreateStorageCommandHandler(IFoodStorageRepository foodStorageRepository)
        {
            _foodStorageRepository = foodStorageRepository;
        }

        /// <inheritdoc />
        public async Task<ICommandResult> Handle(CreateStorageCommand request, CancellationToken cancellationToken)
        {
            var storage = new FoodStorage(request.StorageName, request.Description);

            await _foodStorageRepository.AddAsync(storage);

            //TODO:
            return new CommandResult();
        }
    }
}
