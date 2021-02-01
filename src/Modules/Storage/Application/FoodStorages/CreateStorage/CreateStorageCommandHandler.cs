using FoodVault.Modules.Storage.Domain.FoodStorages;
using FoodVault.Framework.Application.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Application.FoodStorages.CreateStorage
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

            return CommandResult.EntityCreated(storage.Id);
        }
    }
}
