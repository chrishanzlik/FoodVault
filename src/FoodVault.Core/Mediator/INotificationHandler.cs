namespace FoodVault.Core.Mediator
{
    public interface INotificationHandler<in TNotification> : MediatR.INotificationHandler<TNotification>
        where TNotification : INotification
    {
    }
}
