using AirportTool.Infrastructure.Models;

namespace AirportTool.WebAPI.Models.DTOs;

public class BookingDto
{
    public int Id { get; set; }
    public int TicketId { get; set; }
    public string PassengerFullName { get; set; } = null!;
    public string PassengerEmail { get; set; } = null!;
    public string ConfirmationCode { get; set; } = null!;
    public int Quantity { get; set; }
    public DateTime CreatedUtc { get; set; }
    public string Status { get; set; } = null!;
}