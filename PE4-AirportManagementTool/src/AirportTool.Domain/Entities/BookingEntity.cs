using AirportTool.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace AirportTool.Domain.Entities;

public class BookingEntity
{
    protected BookingEntity() { }

    public int Id { get; set; }
    public string PassengerFullName { get; set; } = null!;
    public string PassengerEmail { get; set; } = null!;
    public string ConfirmationCode { get; set; } = null!;
    public int Quantity { get; set; }
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    public virtual BookingStatusEnum BookingStatus { get; set; }
    public virtual TicketEntity Ticket { get; set; } = null!;

    public BookingEntity(
        int id,
        string passengerFullName,
        string passengerEmail,
        string confirmationCode,
        int quantity,
        int bookingStatus,
        TicketEntity ticket
        )
    {
        Id = id;
        PassengerFullName = passengerFullName;
        PassengerEmail = passengerEmail;
        ConfirmationCode = confirmationCode;
        Quantity = quantity;
        BookingStatus = (BookingStatusEnum)bookingStatus;
        Ticket = ticket;
    }
}