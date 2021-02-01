using FoodVault.Framework.Application.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Framework.Application.FileUploads
{
    /// <summary>
    /// Command handler for <see cref="RemoveExpiredFilesCommand"/>.
    /// </summary>
    public class RemoveExpiredFilesCommandHandler : ICommandHandler<RemoveExpiredFilesCommand>
    {
        private readonly IFileStorage _fileStorage;

        public RemoveExpiredFilesCommandHandler(IFileStorage fileStorage)
        {
            _fileStorage = fileStorage;
        }

        public async Task<ICommandResult> Handle(RemoveExpiredFilesCommand request, CancellationToken cancellationToken)
        {
            await _fileStorage.DeleteExpiredFilesAsync();

            return CommandResult.Ok();
        }
    }
}
