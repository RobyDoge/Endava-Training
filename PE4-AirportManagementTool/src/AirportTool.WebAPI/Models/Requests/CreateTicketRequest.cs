using AirportTool.Infrastructure.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportTool.WebAPI.Models.Requests;

public class CreateTicketRequest
{
    [Required]
    [Range(1, int.MaxValue)]
    public int FlightScheduleId { get; set; }

    [Required]
    [StringLength(1, MinimumLength = 1)]
    public string FareClass { get; set; } = null!;

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal BasePrice { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Taxes { get; set; }

    [Required]
    [StringLength(3, MinimumLength = 3)]
    public string Currency { get; set; } = null!;

    public bool? IsRefundable { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int SeatInventory { get; set; }
}