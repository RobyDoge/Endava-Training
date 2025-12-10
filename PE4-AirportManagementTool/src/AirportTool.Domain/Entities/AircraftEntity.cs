namespace AirportTool.Domain.Entities;

public class AircraftEntity
{
    public int Id { get; }
    public string TailName { get; private set; } = null!;
    public string Model { get; private set; } = null!;
    public int SeatCapacity { get; private set; }
    public AirlineEntity AirlineOwner { get; private set; } = null!;

    public AircraftEntity(
        int id,
        string tailName,
        string model,
        int seatCapacity,
        AirlineEntity airlineOwner)
    {
        Id = id;
        TailName = tailName;
        Model = model;
        AirlineOwner = airlineOwner;
        SeatCapacity = seatCapacity;
    }
}