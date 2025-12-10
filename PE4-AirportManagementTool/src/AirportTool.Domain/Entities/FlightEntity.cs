namespace AirportTool.Domain.Entities;

public class FlightEntity
{
    public int Id { get; }
    public string FlightNumber { get; set; } = null!;
    public bool IsActive { get; private set; }
    public AirlineEntity Airline { get; private set; } = null!;
    public AircraftEntity? DefaultAircraft { get; private set; }
    public AirportEntity DestinationAirport { get; private set; } = null!;
    public AirportEntity OriginAirport { get; private set; } = null!;

    public FlightEntity(
        int id,
        string flightNumber,
        AirlineEntity airline,
        AirportEntity originAirport,
        AirportEntity destinationAirport,
        AircraftEntity? defaultAircraft = null,
        bool isActive = true)
    {
        Id = id;
        FlightNumber = flightNumber;
        Airline = airline;
        OriginAirport = originAirport;
        DestinationAirport = destinationAirport;
        DefaultAircraft = defaultAircraft;
        IsActive = isActive;
    }
}