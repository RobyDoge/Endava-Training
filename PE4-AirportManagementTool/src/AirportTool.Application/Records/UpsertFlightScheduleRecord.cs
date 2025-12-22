using System;
using System.Collections.Generic;
using System.Text;

namespace AirportTool.Application.Records;

public record UpsertFlightScheduleRecord
{
    public int? Id { get; init; }
    public int? FlightId { get; init; }
    public DateTime? ScheduledDepartureUtc { get; init; }
    public DateTime? ScheduledArrivalUtc { get; init; }
    public string? GateCode { get; init; } = null!;
    public string? AssignedAircraftTailName { get; init; }
}