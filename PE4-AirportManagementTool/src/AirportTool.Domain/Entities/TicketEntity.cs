using AirportTool.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirportTool.Domain.Entities;

public class TicketEntity
{
    public int Id { get; set; }

    public decimal BasePrice { get; set; }

    public decimal Taxes { get; set; }

    public decimal? TotalPrice { get; set; }

    public bool IsRefundable { get; set; }

    public int? SeatInventory { get; set; }

    public CurrencyEnum Currency { get; set; }

    public FareClassEnum FareClass { get; set; }
    public FlightScheduleEntity FlightSchedule { get; set; } = null!;

    public TicketEntity(int id,
        decimal basePrice,
        decimal taxes,
        int? seatInventory,
        CurrencyEnum currency,
        FareClassEnum fareClass,
        FlightScheduleEntity flightSchedule,
        decimal? totalPrice,
        bool isRefundable = false
        )
    {
        Id = id;
        BasePrice = basePrice;
        Taxes = taxes;
        IsRefundable = isRefundable;
        SeatInventory = seatInventory;
        Currency = currency;
        FareClass = fareClass;
        FlightSchedule = flightSchedule;
        TotalPrice = totalPrice == null ? BasePrice + Taxes : totalPrice;
    }
}