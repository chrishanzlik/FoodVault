using FoodVault.Framework.Application.FileUploads;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Application.Products.AddProductImage
{
    /// <summary>
    /// Notification handler for <see cref="ProductImageAddedNotificationHandler"/>.
    /// </summary>
    internal class ProductImageAddedNotificationHandler : INotificationHandler<ProductImageAddedNotification>
    {
        private readonly IFileStorage _fileStorage;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductImageAddedNotificationHandler" /> class.
        /// </summary>
        /// <param name="fileStorage"></param>
        public ProductImageAddedNotificationHandler(IFileStorage fileStorage)
        {
            _fileStorage = fileStorage;
        }

        /// <inheritdoc />
        public async Task Handle(ProductImageAddedNotification notification, CancellationToken cancellationToken)
        {
            await _fileStorage.PersistFileAsync(notification.ImageId);
        }
    }
}
