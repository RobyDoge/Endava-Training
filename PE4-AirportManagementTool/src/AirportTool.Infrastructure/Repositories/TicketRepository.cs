using AirportTool.Application.Abstractions;
using AirportTool.Application.Records;
using AirportTool.Domain.Entities;
using AirportTool.Domain.Errors;
using AirportTool.Infrastructure.Context;
using AirportTool.Infrastructure.Identifiable;
using AirportTool.Infrastructure.Models;
using AirportTool.Infrastructure.Utils;
using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace AirportTool.Infrastructure.Repositories;

public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
{
    private AirlineBookingContext Context { get; }
    public IEntityHelper EntityHelper { get; }
    private IMapper Mapper { get; }

    public TicketRepository(IMapper mapper, AirlineBookingContext context, IEntityHelper entityHelper) : base(context)
    {
        Mapper = mapper;
        Context = context;
        EntityHelper = entityHelper;
    }

    public async Task<Result<IEnumerable<TicketEntity>, Error>> GetTicketsByFlightAsync(int flightId)
    {
        var result = await Context.Tickets
            .Include(t => t.FlightSchedule)
            .Include(t => t.FareClass)
            .Include(t => t.Currency)
            .Where(t => t.FlightSchedule.FlightId == flightId)
            .ToListAsync();

        return Result.Success<IEnumerable<TicketEntity>, Error>(Mapper.Map<IEnumerable<TicketEntity>>(result));
    }

    public async Task<Result<IIdentifiable<int>, Error>> CreateAsync(CreateTicketRecord record)
    {
        var flightScheduleRes = await EntityHelper.FindRequiredEntity(Context.FlightSchedules, nameof(FlightSchedule.Id), record.FlightScheduleId);
        if (flightScheduleRes.IsFailure) return flightScheduleRes.ConvertFailure<IIdentifiable<int>>();

        var fareClassRes = await EntityHelper.FindRequiredEntity(Context.FareClasses, nameof(FareClass.Id), (int)record.FareClass);
        if (fareClassRes.IsFailure) return fareClassRes.ConvertFailure<IIdentifiable<int>>();

        var currencyRes = await EntityHelper.FindRequiredEntity(Context.Currencies, nameof(Currency.Id), (int)record.Currency);
        if (currencyRes.IsFailure) return currencyRes.ConvertFailure<IIdentifiable<int>>();

        //TODO: Validate seat availability

        var ticket = new Ticket
        {
            FlightSchedule = flightScheduleRes.Value,
            FareClass = fareClassRes.Value,
            Currency = currencyRes.Value,
            BasePrice = record.BasePrice,
            Taxes = record.Taxes,
            IsRefundable = record.IsRefundable ?? false,
            SeatInventory = record.SeatInventory
        };
        var result = await AddAsync(ticket);
        IIdentifiable<int> id = new Identifiable<int>(result, nameof(ticket.Id));
        return Result.Success<IIdentifiable<int>, Error>(id);
    }
}