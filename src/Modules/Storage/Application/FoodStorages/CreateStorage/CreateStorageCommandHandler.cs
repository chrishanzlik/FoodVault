using FoodVault.Modules.Storage.Domain.FoodStorages;
using FoodVault.Framework.Application.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Application.FoodStorages.CreateStorage
{
    /// <summary>
    /// Command handler for the <see cref="CreateStorageCommand"/>.
    /// </summary>
    internal class CreateStorageCommandHandler : ICommandHandler<CreateStorageCommand>
    {
        private readonly IFoodStorageRepository _foodStorageRepository;
        private readonly IStorageNameUniquessChecker _storageNameUniquessChecker;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateStorageCommandHandler" /> class.
        /// </summary>
        /// <param name="foodStorageRepository">FoodStorage repository.</param>
        /// <param name="storageNameUniquessChecker">Domain service that checks that a given storageName is unique.</param>
        public CreateStorageCommandHandler(
            IFoodStorageRepository foodStorageRepository,
            IStorageNameUniquessChecker storageNameUniquessChecker)
        {
            _foodStorageRepository = foodStorageRepository;
            _storageNameUniquessChecker = storageNameUniquessChecker;
        }

        /// <inheritdoc />
        public async Task<ICommandResult> Handle(CreateStorageCommand request, CancellationToken cancellationToken)
        {
            var storage = FoodStorage.CreateNew(request.StorageName, request.Description, _storageNameUniquessChecker);

            await _foodStorageRepository.AddAsync(storage);

            return CommandResult.EntityCreated(storage.Id);
        }
    }
}
