using FoodVault.Application.FileUploads;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Application.Storage.Products.AddProductImage
{
    /// <summary>
    /// Notification handler for <see cref="ProductImageRemovedNotification"/>.
    /// </summary>
    public class ProductImageRemovedNotificationHandler : INotificationHandler<ProductImageRemovedNotification>
    {
        private readonly IFileStorage _fileStorage;

        public ProductImageRemovedNotificationHandler(IFileStorage fileStorage)
        {
            _fileStorage = fileStorage;
        }

        /// <inheritdoc />
        public async Task Handle(ProductImageRemovedNotification notification, CancellationToken cancellationToken)
        {
            await _fileStorage.DeleteFileAsync(notification.ImageId);
        }
    }
}
