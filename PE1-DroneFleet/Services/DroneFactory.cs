using DroneFleet.Enums;
using DroneFleet.Models;

namespace DroneFleet.Services;

internal static class DroneFactory
{
    public static Drone CreateDrone(DroneSpecs specs)
    {
        return specs.Type switch
        {
            DroneType.Survey => new SurveyDrone(specs.Id, specs.Name, specs.BatteryPercent),
            DroneType.Delivery => new DeliveryDrone(specs.Id, specs.Name, specs.BatteryPercent, specs.CapacityKg ?? 0),
            DroneType.Racing => new RacingDrone(specs.Id, specs.Name, specs.BatteryPercent),
            _ => throw new InvalidOperationException("Drone type unsuported.")
        };

    }

}
