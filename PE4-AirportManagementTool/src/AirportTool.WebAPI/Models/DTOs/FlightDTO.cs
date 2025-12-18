namespace AirportTool.WebAPI.Models.DTOs;

public record FlightDTO
{
    public int Id { get; set; }
    public string FlightNumber { get; set; } = null!;
    public string AirlineIata { get; set; } = null!;
    public string OriginAirportIata { get; set; } = null!;
    public string DestinationAirportIata { get; set; } = null!;
    public string? DefaultAircraftTailName { get; set; }
    public bool IsActive { get; set; }
}