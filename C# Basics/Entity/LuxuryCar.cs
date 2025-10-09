using Basics.Base;

namespace Basics.Entity;

public class LuxuryCar: Vehicle
{
    public int MinimumDistanceKm { get; set; } = 5;
    public decimal Multiplier { get; set; } = 0.3m;
}
