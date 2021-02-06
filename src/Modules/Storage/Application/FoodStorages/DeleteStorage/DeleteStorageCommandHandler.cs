using FoodVault.Modules.Storage.Domain.FoodStorages;
using FoodVault.Framework.Application.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Application.FoodStorages.DeleteStorage
{
    /// <summary>
    /// Command handler for the <see cref="DeleteStorageCommand"/>.
    /// </summary>
    internal class DeleteStorageCommandHandler : ICommandHandler<DeleteStorageCommand>
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

            var storage = await _foodStorageRepository.GetByIdAsync(id);

            storage?.Delete();

            return CommandResult.Ok();
        }
    }
}
