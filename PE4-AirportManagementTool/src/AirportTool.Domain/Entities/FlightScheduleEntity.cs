using AirportTool.Domain.Enums;

namespace AirportTool.Domain.Entities;

public class FlightScheduleEntity
{
    public int Id { get; }
    public DateTime ScheduledDepartureUtc { get; private set; }
    public DateTime ScheduledArrivalUtc { get; private set; }
    public FlightEntity Flight { get; private set; } = null!;
    public FlightScheduleStatusEnum Status { get; private set; }
    public GateEntity? Gate { get; private set; } = null!;
    public AircraftEntity? AssignedAircraft { get; private set; }

    public FlightScheduleEntity(
        int id,
        DateTime scheduledDepartureUtc,
        DateTime scheduledArrivalUtc,
        FlightEntity flight,
        FlightScheduleStatusEnum status,
        GateEntity? gate,
        AircraftEntity? assignedAircraft
        )
    {
        Id = id;
        ScheduledArrivalUtc = scheduledArrivalUtc;
        ScheduledDepartureUtc = scheduledDepartureUtc;
        Flight = flight;
        Status = status;
        Gate = gate;
        AssignedAircraft = assignedAircraft;
    }
}