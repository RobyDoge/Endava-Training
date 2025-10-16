using DroneFleet.Enums;

namespace DroneFleet.Services;

internal class DroneSpecs
{
    public DroneType Type { get; init; }
    public int Id { get; init; }
    public int Name { get; init; }
    public decimal BatteryPercent { get; init; }
    public double? CapacityKg { get; init; } = null;
}
