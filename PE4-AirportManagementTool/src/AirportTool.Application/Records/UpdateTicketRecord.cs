using AirportTool.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirportTool.Application.Records;

public class UpdateTicketRecord
{
    public int? FlightScheduleId { get; set; }
    public FareClassEnum? FareClass { get; set; }
    public decimal? BasePrice { get; set; }
    public decimal? Taxes { get; set; }
    public CurrencyEnum? Currency { get; set; }
    public bool? IsRefundable { get; set; }
    public int? SeatInventory { get; set; }
}