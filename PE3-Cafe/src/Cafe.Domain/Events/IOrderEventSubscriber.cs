namespace Cafe.Domain.Events;

public interface IOrderEventSubscriber
{
    Task On(OrderPlaced evt);
}