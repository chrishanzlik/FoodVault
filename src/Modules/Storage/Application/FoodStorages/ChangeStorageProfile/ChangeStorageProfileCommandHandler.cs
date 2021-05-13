using FoodVault.Framework.Application.Commands;
using FoodVault.Modules.Storage.Domain.FoodStorages;
using FoodVault.Modules.Storage.Domain.Users;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Application.FoodStorages.ChangeStorageProfile
{
    /// <summary>
    /// Command handler for the <see cref="ChangeStorageProfileCommand"/>.
    /// </summary>
    internal class ChangeStorageProfileCommandHandler : ICommandHandler<ChangeStorageProfileCommand>
    {
        private readonly IFoodStorageRepository _foodStorageRepository;
        private readonly IStorageNameUniquessChecker _storageNameUniquessChecker;
        private readonly IUserContext _userContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeStorageProfileCommandHandler" /> class.
        /// </summary>
        /// <param name="foodStorageRepository">Food storage repository.</param>
        /// <param name="storageNameUniquessChecker">Domain services that checks that a given storage name is unique.</param>
        /// <param name="userContext">User context.</param>
        public ChangeStorageProfileCommandHandler(
            IFoodStorageRepository foodStorageRepository,
            IStorageNameUniquessChecker storageNameUniquessChecker,
            IUserContext userContext)
        {
            _foodStorageRepository = foodStorageRepository;
            _storageNameUniquessChecker = storageNameUniquessChecker;
            _userContext = userContext;
        }

        /// <inheritdoc />
        public async Task<ICommandResult> Handle(ChangeStorageProfileCommand request, CancellationToken cancellationToken)
        {
            FoodStorageId id = new FoodStorageId(request.FoodStorageId);

            var storage = await _foodStorageRepository.GetByIdAsync(id);

            storage?.Rename(request.StorageName, request.StorageDescription, _userContext, _storageNameUniquessChecker);

            return CommandResult.Ok();
        }
    }
}
