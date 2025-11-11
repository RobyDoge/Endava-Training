namespace Cafe.Domain.Events;

public class OrderPlaced
{
    private Guid OrderId { get; set }
    private DateTimeOffset At { get; set; }
    private string? Description { get; set; }
    private decimal Subtotal { get; set; }
    private decimal Total { get; set; }
}