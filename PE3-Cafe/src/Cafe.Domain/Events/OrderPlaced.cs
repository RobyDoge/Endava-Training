using Cafe.Domain.Beverages;

namespace Cafe.Domain.Events;

public class OrderPlaced
{
    public Guid OrderId { get; } = Guid.NewGuid();
    public DateTimeOffset At { get; } = DateTimeOffset.UtcNow;
    public string? Description { get => Beverage!.Description(); }
    public IBeverage? Beverage { get; set; }
    public decimal Subtotal { get => Beverage!.Cost(); }
    public decimal Total { get; set; }
}