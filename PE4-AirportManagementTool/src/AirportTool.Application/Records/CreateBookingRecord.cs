using System;
using System.Collections.Generic;
using System.Text;

namespace AirportTool.Application.Records;

public record CreateBookingRecord
{
    public int TicketId { get; set; }
    public string PassengerFullName { get; set; } = null!;
    public string PassengerEmail { get; set; } = null!;
    public int Quantity { get; set; }
    public string? ConfirmationCode { get; set; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}