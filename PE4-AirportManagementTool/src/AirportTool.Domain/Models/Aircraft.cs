namespace AirportTool.Domain.Models;

public class Aircraft
{
    public int Id { get; }
    public string TailName { get; private set; } = null!;
    public string Model { get; private set; } = null!;
    public int SeatCapacity { get; private set; }
    public Airline AirlineOwner { get; private set; } = null!;

    public Aircraft(
        int id,
        string tailName,
        string model,
        int seatCapacity,
        Airline airlineOwner)
    {
        Id = id;
        TailName = tailName;
        Model = model;
        AirlineOwner = airlineOwner;
        SeatCapacity = seatCapacity;
    }
}