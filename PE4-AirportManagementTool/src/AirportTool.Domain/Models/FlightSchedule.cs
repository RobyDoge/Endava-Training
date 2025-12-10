using AirportTool.Domain.Enums;

namespace AirportTool.Domain.Models;

public class FlightSchedule
{
    public int Id { get; }
    public DateTime ScheduledDepartureUtc { get; set; }
    public DateTime ScheduledArrivalUtc { get; set; }
    public Flight Flight { get; set; } = null!;
    public FlightScheduleStatus Status { get; set; }
    public Gate Gate { get; set; } = null!;

    public FlightSchedule(
        int id,
        DateTime scheduledDepartureUtc,
        DateTime scheduledArrivalUtc,
        Flight flight,
        FlightScheduleStatus status,
        Gate gate
        )
    {
        Id = id;
        ScheduledArrivalUtc = scheduledArrivalUtc;
        ScheduledDepartureUtc = scheduledDepartureUtc;
        Flight = flight;
        Status = status;
        Gate = gate;
    }
}