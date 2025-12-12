using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AirportTool.WebAPI.Models.Requests;

public class CreateFlightRequest
{
    [Required]
    [StringLength(2, MinimumLength = 2)]
    public string AirlineIata { get; set; } = null!;

    [Required]
    [StringLength(8)]
    public string FlightNumber { get; set; } = null!;

    [Required]
    [StringLength(3, MinimumLength = 3)]
    public string OriginAirportIata { get; set; } = null!;

    [Required]
    [StringLength(3, MinimumLength = 3)]
    public string DestinationAirportIata { get; set; } = null!;

    public string? DefaultAircraftTailName { get; set; }
}