namespace AirportTool.Domain.Models;

public class Flight
{
    public int Id { get; }
    public string FlightNumber { get; set; } = null!;
    public bool IsActive { get; private set; }
    public Airline Airline { get; private set; } = null!;
    public Aircraft? DefaultAircraft { get; private set; }
    public Airport DestinationAirport { get; private set; } = null!;
    public Airport OriginAirport { get; private set; } = null!;

    public Flight(
        int id,
        string flightNumber,
        Airline airline,
        Airport originAirport,
        Airport destinationAirport,
        Aircraft? defaultAircraft = null,
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