using System.ComponentModel.DataAnnotations;

namespace AirportTool.WebAPI.Models.Requests;

public class CreateBookingRequest
{
    [Required]
    [Range(1, int.MaxValue)]
    public int TicketId { get; set; }

    [Required]
    [MaxLength(120)]
    public string PassengerFullName { get; set; } = null!;

    [Required]
    [EmailAddress, MaxLength(120)]
    public string PassengerEmail { get; set; } = null!;

    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}