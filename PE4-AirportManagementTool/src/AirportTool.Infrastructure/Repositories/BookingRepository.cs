using AirportTool.Application.Abstractions;
using AirportTool.Application.Records;
using AirportTool.Application.Services;
using AirportTool.Domain.Entities;
using AirportTool.Domain.Enums;
using AirportTool.Domain.Errors;
using AirportTool.Infrastructure.Context;
using AirportTool.Infrastructure.Identifiable;
using AirportTool.Infrastructure.Models;
using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirportTool.Infrastructure.Repositories;

public class BookingRepository : GenericRepository<Booking>, IBookingRepository
{
    private AirlineBookingContext Context { get; }
    private IMapper Mapper { get; }
    public IEntityHelper EntityHelper { get; }

    public BookingRepository(AirlineBookingContext context, IMapper mapper, IEntityHelper entityHelper) : base(context)
    {
        Context = context;
        Mapper = mapper;
        EntityHelper = entityHelper;
    }

    public async Task<UnitResult<Error>> CancelAsync(string confimationCode)
    {
        var booking = await Context.Bookings
            .Include(b => b.Ticket)
            .SingleOrDefaultAsync(b => b.ConfirmationCode == confimationCode);
        if (booking == null) return UnitResult.Failure<Error>(Error.NotFound(nameof(Booking), confimationCode));

        booking.BookingStatusId = (int)BookingStatusEnum.Cancelled;
        booking.Ticket.SeatInventory += booking.Quantity;

        return UnitResult.Success<Error>();
    }

    public async Task<Result<BookingEntity, Error>> GetByConfirmationCodeAsync(string confirmationCode)
    {
        var booking = await Context.Bookings
            .Include(b => b.Ticket)
            .Include(b => b.BookingStatus)
            .SingleOrDefaultAsync(b => b.ConfirmationCode == confirmationCode);

        if (booking == null) return Result.Failure<BookingEntity, Error>(Error.NotFound(nameof(Booking), confirmationCode));

        return Mapper.Map<BookingEntity>(booking);
    }

    public async Task<Result<BookingEntity, Error>> GetAsync(int id)
    {
        var booking = await Context.Bookings
            .Include(b => b.Ticket)
            .Include(b => b.BookingStatus)
            .SingleOrDefaultAsync(b => b.Id == id);

        if (booking == null) return Result.Failure<BookingEntity, Error>(Error.NotFound(nameof(Booking), id));

        return Mapper.Map<BookingEntity>(booking);
    }

    public async Task<Result<IIdentifiable<int>, Error>> CreateAsync(CreateBookingRecord record)
    {
        var ticketRes = await EntityHelper.FindEntityAsync(Context.Tickets, nameof(Ticket.Id), record.TicketId);
        if (ticketRes.IsFailure) return ticketRes.ConvertFailure<IIdentifiable<int>>();

        //by default the staus is created with status 1 - Active
        var statusId = 1;
        var statusRes = await EntityHelper.FindEntityAsync(Context.BookingStatuses, nameof(BookingStatus.Id), statusId);
        if (statusRes.IsFailure) return statusRes.ConvertFailure<IIdentifiable<int>>();

        if (ticketRes.Value.SeatInventory < record.Quantity)
        {
            return Result.Failure<IIdentifiable<int>, Error>(Error.Validation($"Not enough seats available. Requested: {record.Quantity}, Available: {ticketRes.Value.SeatInventory}"));
        }
        ticketRes.Value.SeatInventory -= record.Quantity;

        var booking = new Booking
        {
            Ticket = ticketRes.Value,
            BookingStatus = statusRes.Value,
            PassengerEmail = record.PassengerEmail,
            PassengerFullName = record.PassengerFullName,
            ConfirmationCode = record!.ConfirmationCode,
            CreatedUtc = record.CreatedAt,
            Quantity = record.Quantity
        };

        var result = await AddAsync(booking);
        IIdentifiable<int> id = new Identifiable<int>(result, nameof(booking.Id));
        return Result.Success<IIdentifiable<int>, Error>(id);
    }
}