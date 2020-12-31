namespace FoodVault.Application.Mediator
{
    public interface INotificationHandler<in TNotification> : MediatR.INotificationHandler<TNotification>
        where TNotification : INotification
    {
    }
}
