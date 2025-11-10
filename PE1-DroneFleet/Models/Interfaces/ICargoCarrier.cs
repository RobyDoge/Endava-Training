namespace DroneFleet.Models.Interfaces;

internal interface ICargoCarrier
{
    double CapacityKg { get; }
    double CurrentLoadKg { get; }

    bool Load(double weightKg);

    void UnloadAll();
}