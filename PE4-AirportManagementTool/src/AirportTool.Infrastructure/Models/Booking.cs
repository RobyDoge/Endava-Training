using System;
using System.Collections.Generic;

namespace AirportTool.Infrastructure.Models;

public partial class Booking
{
    public int Id { get; set; }

    public int TicketId { get; set; }

    public string PassengerFullName { get; set; } = null!;

    public string PassengerEmail { get; set; } = null!;

    public string ConfirmationCode { get; set; } = null!;

    public int Quantity { get; set; }

    public int BookingStatusId { get; set; }

    public DateTime CreatedUtc { get; set; }

    public virtual BookingStatus BookingStatus { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;
}
