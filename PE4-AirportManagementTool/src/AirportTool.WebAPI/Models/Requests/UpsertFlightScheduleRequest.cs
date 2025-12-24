using System.ComponentModel.DataAnnotations;

namespace AirportTool.WebAPI.Models.Requests;

public class UpsertFlightScheduleRequest
{
    [Range(1, int.MaxValue)]]
    public int? Id { get; init; }
    [Range(1, int.MaxValue)]
    public int? FlightId { get; init; }
    public DateTime? ScheduledDepartureUtc { get; init; }
    public DateTime? ScheduledArrivalUtc { get; init; }
    [MaxLength(10)]
    public string? GateCode { get; init; } = null!;
    [MaxLength(10)]
    public string? AssignedAircraftTailName { get; init; }
}