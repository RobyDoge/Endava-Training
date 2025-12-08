using System;
using System.Collections.Generic;

namespace AirportTool.Infrastructure.Models;

public partial class FlightScheduleStatus
{
    public int Id { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<FlightSchedule> FlightSchedules { get; set; } = new List<FlightSchedule>();
}
