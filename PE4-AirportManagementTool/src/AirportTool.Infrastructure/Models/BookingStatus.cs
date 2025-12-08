using System;
using System.Collections.Generic;

namespace AirportTool.Infrastructure.Models;

public partial class BookingStatus
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
