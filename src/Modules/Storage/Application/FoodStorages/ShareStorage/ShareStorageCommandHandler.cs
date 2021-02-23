using FoodVault.Framework.Application.Commands;
using FoodVault.Framework.Application.DataAccess;
using FoodVault.Modules.Storage.Domain.FoodStorages;
using FoodVault.Modules.Storage.Domain.Users;
using System.Linq;
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
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShareStorageCommandHandler" /> class.
        /// </summary>
        /// <param name="userContext">Acces to the current user session.</param>
        /// <param name="foodStorageRepository">Food storage repository.</param>
        /// <param name="dbConnectionFactory">Db connection factory.</param>
        public ShareStorageCommandHandler(
            IUserContext userContext,
            IFoodStorageRepository foodStorageRepository,
            IDbConnectionFactory dbConnectionFactory)
        {
            _userContext = userContext;
            _foodStorageRepository = foodStorageRepository;
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <inheritdoc />
        public async Task<ICommandResult> Handle(ShareStorageCommand request, CancellationToken cancellationToken)
        {
            var storage = await _foodStorageRepository.GetByIdAsync(new FoodStorageId(request.FoodStorageId));
            if (storage == null)
            {
                return CommandResult.BadParameters(new[] { $"The storage with the id '{request.FoodStorageId}' does not exist." });
            }

            var sharedUsers = (await StorageSharesProvider.GetSharesForStorageAsync(
                request.FoodStorageId,
                _dbConnectionFactory)).Select(x => new UserId(x.UserId));

            storage.ShareToUser(
                new UserId(request.UserId),
                request.WriteAccess,
                sharedUsers,
                _userContext);

            return CommandResult.Ok();
        }
    }
}
