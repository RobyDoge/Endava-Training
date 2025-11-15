namespace Cafe.Domain.Events;

public interface IOrderEventPublisher
{
    void Subscribe(IOrderEventSubscriber subscriber);

    Task Publish(OrderPlaced evt);
}