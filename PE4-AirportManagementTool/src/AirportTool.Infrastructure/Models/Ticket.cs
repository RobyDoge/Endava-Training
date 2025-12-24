using System;
using System.Collections.Generic;

namespace AirportTool.Infrastructure.Models;

public partial class Ticket
{
    public int Id { get; set; }

    public int FlightScheduleId { get; set; }

    public int FareClassId { get; set; }

    public decimal BasePrice { get; set; }

    public decimal Taxes { get; set; }

    public decimal? TotalPrice { get; set; }

    public int CurrencyId { get; set; }

    public bool IsRefundable { get; set; }

    public int SeatInventory { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Currency Currency { get; set; } = null!;

    public virtual FareClass FareClass { get; set; } = null!;

    public virtual FlightSchedule FlightSchedule { get; set; } = null!;
}
