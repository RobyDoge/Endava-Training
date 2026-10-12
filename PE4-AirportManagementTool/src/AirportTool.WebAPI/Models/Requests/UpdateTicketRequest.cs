using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportTool.WebAPI.Models.Requests;

public class UpdateTicketRequest
{
    [Range(1, int.MaxValue)]
    public int? FlightScheduleId { get; set; }

    [Required]
    [StringLength(1, MinimumLength = 1)]
    public string? FareClass { get; set; } = null!;

    [Column(TypeName = "decimal(10,2)")]
    public decimal? BasePrice { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal? Taxes { get; set; }

    [StringLength(3, MinimumLength = 3)]
    public string? Currency { get; set; } = null!;

    public bool? IsRefundable { get; set; }

    [Range(1, int.MaxValue)]
    public int? SeatInventory { get; set; }
}