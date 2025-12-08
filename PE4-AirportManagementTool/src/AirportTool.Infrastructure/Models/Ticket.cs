using System;
using System.Collections.Generic;

namespace AirportTool.Infrastructure.Models;

public partial class Ticket
{
    public int Id { get; set; }

    public int FlightScheduleId { get; set; }

    public string FareClass { get; set; } = null!;

    public decimal BasePrice { get; set; }

    public decimal Taxes { get; set; }

    public decimal? TotalPrice { get; set; }

    public string CurrencyCode { get; set; } = null!;

    public bool IsRefundable { get; set; }

    public int? SeatInventory { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Currency CurrencyCodeNavigation { get; set; } = null!;

    public virtual FareClass FareClassNavigation { get; set; } = null!;

    public virtual FlightSchedule FlightSchedule { get; set; } = null!;
}
