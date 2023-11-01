using MediatR;

namespace WebApp.SharedServer.Notifications;

public interface IAppNotificationPublisher
{
    Task Publish(IEnumerable<INotification> notifications);

}



public class AppNotificationPublisher : IAppNotificationPublisher
{
    private readonly IPublisher _publisher;
    private readonly List<INotification> _notifications;
    public AppNotificationPublisher(IPublisher publisher)
    {
        _publisher = publisher;
        _notifications = new List<INotification>();
    }

    public void Add(INotification notification)
    {
        _notifications.Add(notification);
    }
    public async Task Publish(IEnumerable<INotification> notifications)
    {
        if (notifications.Count() > 0)
        {
            foreach (var domainEvent in notifications)
            {
                //publish only if dispense log
                await _publisher.Publish(domainEvent);
            }
        }
    }

}