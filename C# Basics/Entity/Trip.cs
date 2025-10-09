using Basics.Enum;

namespace Basics.Entity;

public class Trip
{
    public Guid Id { get; } = Guid.NewGuid();
    public Guid PassangerId { get; set; }
    public Guid DriverId { get; set; }
    public decimal DistanceKm { get; set; }
    public decimal Cost { get; set; }
    public TripStatus Status { get; set; } = TripStatus.Unknown;
}
