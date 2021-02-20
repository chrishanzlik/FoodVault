using FoodVault.Framework.Application.Commands;
using FoodVault.Modules.Storage.Domain.FoodStorages;
using FoodVault.Modules.Storage.Domain.Users;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Application.FoodStorages.ShareStorage
{
    /// <summary>
    /// Command handler for the <see cref="ShareStorageCommand"/>.
    /// </summary>
    internal class ShareStorageCommandHandler : ICommandHandler<ShareStorageCommand>
    {
        private readonly IUserContext _userContext;
        private readonly IFoodStorageRepository _foodStorageRepository;
        private readonly IStorageUserSharesFinder _userSharesFinder;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShareStorageCommandHandler" /> class.
        /// </summary>
        /// <param name="userContext">Acces to the current user session.</param>
        /// <param name="foodStorageRepository">Food storage repository.</param>
        /// <param name="userSharesFinder">Get all shared user ids for a specific storage.</param>
        public ShareStorageCommandHandler(
            IUserContext userContext,
            IFoodStorageRepository foodStorageRepository,
            IStorageUserSharesFinder userSharesFinder)
        {
            _userContext = userContext;
            _foodStorageRepository = foodStorageRepository;
            _userSharesFinder = userSharesFinder;
        }

        /// <inheritdoc />
        public async Task<ICommandResult> Handle(ShareStorageCommand request, CancellationToken cancellationToken)
        {
            FoodStorageId foodStorageId = new FoodStorageId(request.FoodStorageId);
            UserId userId = new UserId(request.UserId);

            var storage = await _foodStorageRepository.GetByIdAsync(foodStorageId);
            storage?.Share(userId, request.WriteAccess, _userSharesFinder, _userContext);

            return CommandResult.Ok();
        }
    }
}
