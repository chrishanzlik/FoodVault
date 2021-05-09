using FoodVault.Modules.Storage.Domain.FoodStorages;
using FoodVault.Framework.Application.Commands;
using System.Threading;
using System.Threading.Tasks;
using FoodVault.Framework.Application;
using FoodVault.Modules.Storage.Domain.Users;

namespace FoodVault.Modules.Storage.Application.FoodStorages.CreateStorage
{
    /// <summary>
    /// Command handler for the <see cref="CreateStorageCommand"/>.
    /// </summary>
    internal class CreateStorageCommandHandler : ICommandHandler<CreateStorageCommand>
    {
        private readonly IFoodStorageRepository _foodStorageRepository;
        private readonly IStorageNameUniquessChecker _storageNameUniquessChecker;
        private readonly IExecutionContextAccessor _executionContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateStorageCommandHandler" /> class.
        /// </summary>
        /// <param name="foodStorageRepository">FoodStorage repository.</param>
        /// <param name="storageNameUniquessChecker">Domain service that checks that a given storageName is unique.</param>
        /// <param name="executionContextAccessor">Accesor for user session.</param>
        public CreateStorageCommandHandler(
            IFoodStorageRepository foodStorageRepository,
            IStorageNameUniquessChecker storageNameUniquessChecker,
            IExecutionContextAccessor executionContextAccessor)
        {
            _foodStorageRepository = foodStorageRepository;
            _storageNameUniquessChecker = storageNameUniquessChecker;
            _executionContextAccessor = executionContextAccessor;
        }

        /// <inheritdoc />
        public async Task<ICommandResult> Handle(CreateStorageCommand request, CancellationToken cancellationToken)
        {
            if(!_executionContextAccessor.IsAvailable)
            {
                return CommandResult.Error(new string[] { "No user logged in." });
            }

            UserId userId = new UserId(_executionContextAccessor.UserId);

            var storage = FoodStorage.CreateForUser(userId, request.StorageName, request.Description, _storageNameUniquessChecker);

            await _foodStorageRepository.AddAsync(storage);

            return CommandResult.EntityCreated(storage.Id);
        }
    }
}
