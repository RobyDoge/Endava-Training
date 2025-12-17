using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportTool.Application.Records;

public record CreateTicketRecord
{
    public int FlightScheduleId { get; set; }
    public string FareClass { get; set; } = null!;
    public decimal BasePrice { get; set; }
    public decimal Taxes { get; set; }
    public string Currency { get; set; } = null!;
    public bool? IsRefundable { get; set; }
    public int SeatInventory { get; set; }
}