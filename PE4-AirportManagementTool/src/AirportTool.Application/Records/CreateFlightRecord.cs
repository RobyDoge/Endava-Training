namespace AirportTool.Application.Records;

public record CreateFlightRecord
{
    public string AirlineIata { get; init; } = null!;
    public string FlightNumber { get; init; } = null!;
    public string OriginAirportIata { get; init; } = null!;
    public string DestinationAirportIata { get; init; } = null!;
    public string? DefaultAircraftTailName { get; init; }
}