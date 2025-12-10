using AirportTool.Domain.Enums;

namespace AirportTool.Domain.Entities;

public class FlightScheduleEntity
{
    public int Id { get; }
    public DateTime ScheduledDepartureUtc { get; set; }
    public DateTime ScheduledArrivalUtc { get; set; }
    public FlightEntity Flight { get; set; } = null!;
    public FlightScheduleStatusEnum Status { get; set; }
    public GateEntity Gate { get; set; } = null!;

    public FlightScheduleEntity(
        int id,
        DateTime scheduledDepartureUtc,
        DateTime scheduledArrivalUtc,
        FlightEntity flight,
        FlightScheduleStatusEnum status,
        GateEntity gate
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