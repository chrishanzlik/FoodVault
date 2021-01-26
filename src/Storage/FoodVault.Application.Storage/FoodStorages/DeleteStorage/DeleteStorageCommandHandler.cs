using FoodVault.Application.Commands;
using FoodVault.Domain.Storage.FoodStorages;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Application.Storage.FoodStorages.DeleteStorage
{
    /// <summary>
    /// Command handler for the <see cref="DeleteStorageCommand"/>.
    /// </summary>
    public class DeleteStorageCommandHandler : ICommandHandler<DeleteStorageCommand>
    {
        private readonly IFoodStorageRepository _foodStorageRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteStorageCommandHandler" /> class.
        /// </summary>
        /// <param name="foodStorageRepository">FoodStorage repository.</param>
        public DeleteStorageCommandHandler(IFoodStorageRepository foodStorageRepository)
        {
            _foodStorageRepository = foodStorageRepository;
        }

        /// <inheritdoc />
        public async Task<ICommandResult> Handle(DeleteStorageCommand request, CancellationToken cancellationToken)
        {
            FoodStorageId id = new FoodStorageId(request.FoodStorageId);

            await _foodStorageRepository.RemoveAsync(id);

            return CommandResult.Ok();
        }
    }
}
