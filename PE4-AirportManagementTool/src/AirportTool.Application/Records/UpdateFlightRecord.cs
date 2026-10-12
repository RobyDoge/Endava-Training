namespace AirportTool.Application.Records;

public record UpdateFlightRecord
{
    public string? AirlineIata { get; set; }
    public string? FlightNumber { get; set; }
    public string? OriginAirportIata { get; set; }
    public string? DestinationAirportIata { get; set; }
    public string? DefaultAircraftTailName { get; set; }
    public bool? IsActive { get; set; }
}