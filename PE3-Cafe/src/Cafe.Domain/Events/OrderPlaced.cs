using Cafe.Domain.Beverages;

namespace Cafe.Domain.Events;

public class OrderPlaced
{
    private Guid OrderId { get; } = Guid.NewGuid();
    private DateTimeOffset At { get; } = DateTimeOffset.UtcNow;
    public string? Description { get => Beverage!.Description(); }
    public IBeverage? Beverage { get; set; }
    public decimal Subtotal { get => Beverage!.Cost(); }
    public decimal Total { get; set; }
}