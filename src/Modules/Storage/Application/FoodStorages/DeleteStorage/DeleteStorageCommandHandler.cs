using FoodVault.Modules.Storage.Domain.FoodStorages;
using FoodVault.Framework.Application.Commands;
using System.Threading;
using System.Threading.Tasks;
using FoodVault.Modules.Storage.Domain.Users;

namespace FoodVault.Modules.Storage.Application.FoodStorages.DeleteStorage
{
    /// <summary>
    /// Command handler for the <see cref="DeleteStorageCommand"/>.
    /// </summary>
    internal class DeleteStorageCommandHandler : ICommandHandler<DeleteStorageCommand>
    {
        private readonly IFoodStorageRepository _foodStorageRepository;
        private readonly IUserContext _userContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteStorageCommandHandler" /> class.
        /// </summary>
        /// <param name="foodStorageRepository">FoodStorage repository.</param>
        /// <param name="userContext">User context.</param>
        public DeleteStorageCommandHandler(IFoodStorageRepository foodStorageRepository, IUserContext userContext)
        {
            _foodStorageRepository = foodStorageRepository;
            _userContext = userContext;
        }

        /// <inheritdoc />
        public async Task<ICommandResult> Handle(DeleteStorageCommand request, CancellationToken cancellationToken)
        {
            FoodStorageId id = new FoodStorageId(request.FoodStorageId);

            var storage = await _foodStorageRepository.GetByIdAsync(id);

            storage?.Delete(_userContext);

            return CommandResult.Ok();
        }
    }
}
