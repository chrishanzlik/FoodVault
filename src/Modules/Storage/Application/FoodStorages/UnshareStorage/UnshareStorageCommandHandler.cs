using FoodVault.Framework.Application.Commands;
using FoodVault.Modules.Storage.Domain.FoodStorages;
using FoodVault.Modules.Storage.Domain.Users;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Application.FoodStorages.UnshareStorage
{
    /// <summary>
    /// Command handler for the <see cref="UnshareStorageCommand"/>.
    /// </summary>
    internal class UnshareStorageCommandHandler : ICommandHandler<UnshareStorageCommand>
    {
        private readonly IUserContext _userContext;
        private readonly IFoodStorageRepository _foodStorageRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnshareStorageCommandHandler" /> class.
        /// </summary>
        /// <param name="userContext">Provides access to the user session.</param>
        /// <param name="foodStorageRepository">Food storage repository.</param>
        public UnshareStorageCommandHandler(IUserContext userContext, IFoodStorageRepository foodStorageRepository)
        {
            _userContext = userContext;
            _foodStorageRepository = foodStorageRepository;
        }

        /// <inheritdoc />
        public async Task<ICommandResult> Handle(UnshareStorageCommand request, CancellationToken cancellationToken)
        {
            FoodStorageId foodStorageId = new FoodStorageId(request.FoodStorageId);
            UserId userId = new UserId(request.UserId);

            var storage = await _foodStorageRepository.GetByIdAsync(foodStorageId);
            storage?.Unshare(userId, _userContext);

            return CommandResult.Ok();
        }
    }
}
