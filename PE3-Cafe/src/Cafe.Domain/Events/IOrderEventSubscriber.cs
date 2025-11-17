namespace Cafe.Domain.Events;

public interface IOrderEventSubscriber
{
    Task On(Order evt);
}