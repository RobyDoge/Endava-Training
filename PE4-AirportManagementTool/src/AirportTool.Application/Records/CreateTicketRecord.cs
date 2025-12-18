using AirportTool.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportTool.Application.Records;

public record CreateTicketRecord
{
    public int FlightScheduleId { get; set; }
    public FareClassEnum FareClass { get; set; }
    public decimal BasePrice { get; set; }
    public decimal Taxes { get; set; }
    public CurrencyEnum Currency { get; set; }
    public bool? IsRefundable { get; set; }
    public int SeatInventory { get; set; }
}