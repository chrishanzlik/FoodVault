using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Application.Storage.Products.AddProductImage
{
    /// <summary>
    /// Notification handler for <see cref="ProductImageRemovedNotification"/>.
    /// </summary>
    public class ProductImageRemovedNotificationHandler : INotificationHandler<ProductImageRemovedNotification>
    {
        public Task Handle(ProductImageRemovedNotification notification, CancellationToken cancellationToken)
        {
            //TODO: Remove image from disk

            throw new NotImplementedException();
        }
    }
}
