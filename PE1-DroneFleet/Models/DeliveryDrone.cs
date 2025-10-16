using DroneFleet.Models.Interfaces;

namespace DroneFleet.Models;

internal class DeliveryDrone : Drone, INavigable, ICargoCarrier
{
    public (double latitude, double longitude)? CurrentWaypoint { get; private set; } = null;

    public double CapacityKg { get; init; }
    public double CurrentLoadKg { get; private set; } = 0;

    public DeliveryDrone(int id, string name, decimal batteryPercent, double capacityKg) : base(id, name, batteryPercent)
    {
        if(capacityKg<= 0) throw new ArgumentOutOfRangeException(nameof(capacityKg), "The capacity must be greater than 0.");
        CapacityKg = capacityKg;
    }

    public void SetWaypoint(double latitude, double longitude)
    {
        CurrentWaypoint = (latitude, longitude);
    }

    public bool Load(double weightKg)
    {
        if (CurrentLoadKg + weightKg > CapacityKg) return false;
        CurrentLoadKg += weightKg;
        return true;
    }

    public void UnloadAll()
    {
        CurrentLoadKg = 0;
    }
}
