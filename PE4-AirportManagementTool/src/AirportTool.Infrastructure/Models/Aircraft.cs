using System;
using System.Collections.Generic;

namespace AirportTool.Infrastructure.Models;

public partial class Aircraft
{
    public int Id { get; set; }

    public string TailName { get; set; } = null!;

    public string Model { get; set; } = null!;

    public int SeatCapacity { get; set; }

    public int? AirlineOwnerId { get; set; }

    public virtual Airline? AirlineOwner { get; set; }

    public virtual ICollection<FlightSchedule> FlightSchedules { get; set; } = new List<FlightSchedule>();

    public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();
}
