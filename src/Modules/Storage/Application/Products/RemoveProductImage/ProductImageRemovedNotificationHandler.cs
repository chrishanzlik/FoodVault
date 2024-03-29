﻿using FoodVault.Framework.Application.FileUploads;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Application.Products.RemoveProductImage
{
    /// <summary>
    /// Notification handler for <see cref="ProductImageRemovedNotification"/>.
    /// </summary>
    internal class ProductImageRemovedNotificationHandler : INotificationHandler<ProductImageRemovedNotification>
    {
        private readonly IFileStorage _fileStorage;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductImageRemovedNotificationHandler" /> class.
        /// </summary>
        /// <param name="fileStorage">Applications file storage.</param>
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
