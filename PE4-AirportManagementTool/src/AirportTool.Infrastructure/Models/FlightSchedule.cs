using System;
using System.Collections.Generic;

namespace AirportTool.Infrastructure.Models;

public partial class FlightSchedule
{
    public int Id { get; set; }

    public int FlightId { get; set; }

    public DateTime ScheduledDepartureUtc { get; set; }

    public DateTime ScheduledArrivalUtc { get; set; }

    public int GateId { get; set; }

    public int? AssignedAircraftId { get; set; }

    public int Status { get; set; }

    public virtual Flight Flight { get; set; } = null!;

    public virtual Gate Gate { get; set; } = null!;

    public virtual FlightScheduleStatus StatusNavigation { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
