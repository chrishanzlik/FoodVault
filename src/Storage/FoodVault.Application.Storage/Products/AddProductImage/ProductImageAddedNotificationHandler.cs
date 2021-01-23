﻿using FoodVault.Application.FileUploads;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Application.Storage.Products.AddProductImage
{
    /// <summary>
    /// Notification handler for <see cref="ProductImageAddedNotificationHandler"/>.
    /// </summary>
    public class ProductImageAddedNotificationHandler : INotificationHandler<ProductImageAddedNotification>
    {
        private readonly IFileStorage _fileStorage;

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