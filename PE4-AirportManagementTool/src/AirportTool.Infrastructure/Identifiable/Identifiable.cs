using AirportTool.Application.Abstractions;

namespace AirportTool.Infrastructure.Identifiable;

public class Identifiable<T> : IIdentifiable<T>
{
    private dynamic Item { get; }
    private string PropertyName { get; }

    public Identifiable(dynamic item, string propertyName)
    {
        Item = item;
        PropertyName = propertyName;
    }

    public T Id { get => (T)Item.GetType().GetProperty(PropertyName).GetValue(Item, null); }
}