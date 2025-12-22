namespace AirportTool.WebAPI.Models.Requests;

public class UpsertFlightScheduleRequest
{
    public int? Id { get; init; }
    public int? FlightId { get; init; }
    public DateTime? ScheduledDepartureUtc { get; init; }
    public DateTime? ScheduledArrivalUtc { get; init; }
    public string? GateCode { get; init; } = null!;
    public string? AssignedAircraftTailName { get; init; }
}