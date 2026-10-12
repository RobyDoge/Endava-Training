using AirportTool.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirportTool.Application.Records;

public record UpdateFlightScheduleRecord
{
    public int Id { get; set; }
    public int? FlightId { get; init; }
    public DateTime? ScheduledDepartureUtc { get; init; }
    public DateTime? ScheduledArrivalUtc { get; init; }
    public string? GateCode { get; init; } = null!;
    public string? AssignedAircraftTailName { get; init; }
    public FlightScheduleStatusEnum? FlightScheduleStatus { get; init; }
}