using System;
using System.Collections.Generic;
using System.Text;

namespace FoodVault.Core.Mediator
{
    public interface INotificationHandler<in TNotification> : MediatR.INotificationHandler<TNotification>
        where TNotification : INotification
    {
    }
}
