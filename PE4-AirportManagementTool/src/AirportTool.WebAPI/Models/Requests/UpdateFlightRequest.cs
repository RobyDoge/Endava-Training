using System.ComponentModel.DataAnnotations;

namespace AirportTool.WebAPI.Models.Requests;

public class UpdateFlightRequest
{
    [StringLength(2, MinimumLength = 2)]
    public string? AirlineIata { get; set; }

    [StringLength(8)]
    public string? FlightNumber { get; set; }

    [StringLength(3, MinimumLength = 3)]
    public string? OriginAirportIata { get; set; }

    [StringLength(3, MinimumLength = 3)]
    public string? DestinationAirportIata { get; set; }

    public string? DefaultAircraftTailName { get; set; }
    public bool IsActive { get; set; }
}