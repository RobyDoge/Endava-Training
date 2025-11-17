namespace Cafe.Domain.Events;

public interface IOrderEventPublisher
{
    void Subscribe(IOrderEventSubscriber subscriber);

    Task Publish(Order evt);
}