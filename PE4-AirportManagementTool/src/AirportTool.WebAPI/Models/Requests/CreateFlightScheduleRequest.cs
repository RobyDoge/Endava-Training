using System.ComponentModel.DataAnnotations;

namespace AirportTool.WebAPI.Models.Requests;

public class CreateFlightScheduleRequest
{
    [Required]
    [Range(1, int.MaxValue)]
    public int FlightId { get; init; }

    [Required]
    public DateTime ScheduledDepartureUtc { get; init; }

    [Required]
    public DateTime ScheduledArrivalUtc { get; init; }

    [Required]
    [MaxLength(10)]
    public string GateCode { get; init; } = null!;

    public string? AssignedAircraftTailName { get; init; }
}