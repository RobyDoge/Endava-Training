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

    public TicketRepository(IMapper mapper, AirlineBookingContext context, IEntityHelper entityHelper) : base(context, mapper)
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
        var flightScheduleRes = await EntityHelper.FindEntityAsync(Context.FlightSchedules, nameof(FlightSchedule.Id), record.FlightScheduleId);
        if (flightScheduleRes.IsFailure) return flightScheduleRes.ConvertFailure<IIdentifiable<int>>();

        var fareClassRes = await EntityHelper.FindEntityAsync(Context.FareClasses, nameof(FareClass.Id), (int)record.FareClass);
        if (fareClassRes.IsFailure) return fareClassRes.ConvertFailure<IIdentifiable<int>>();

        var currencyRes = await EntityHelper.FindEntityAsync(Context.Currencies, nameof(Currency.Id), (int)record.Currency);
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

    public async Task<UnitResult<Error>> UpdateAsync(int id, UpdateTicketRecord record)
    {
        var ticket = await Context.Tickets
            .Include(t => t.FlightSchedule)
            .Include(t => t.FareClass)
            .Include(t => t.Currency)
            .SingleOrDefaultAsync(t => t.Id == id);
        if (ticket is null) return UnitResult.Failure(Error.NotFound(nameof(Ticket), id));

        var flightScheduleRes = await EntityHelper.FindEntityAsync(Context.FlightSchedules, nameof(FlightSchedule.Id), record.FlightScheduleId);
        if (flightScheduleRes.IsFailure) return flightScheduleRes;

        if (record.FareClass is not null)
        {
            var fareClassRes = await EntityHelper.FindEntityAsync(Context.FareClasses, nameof(FareClass.Id), (int)record.FareClass);
            if (fareClassRes.IsFailure) return fareClassRes.ConvertFailure<IIdentifiable<int>>();
            EntityHelper.Patch(record.FareClass, v => ticket.FareClass = fareClassRes.Value);
        }

        if (record.Currency is not null)
        {
            var currencyRes = await EntityHelper.FindEntityAsync(Context.Currencies, nameof(Currency.Id), (int)record.Currency);
            if (currencyRes.IsFailure) return currencyRes.ConvertFailure<IIdentifiable<int>>();
            EntityHelper.Patch(record.Currency, v => ticket.Currency = currencyRes.Value);
        }

        EntityHelper.Patch(record.FlightScheduleId, v => ticket.FlightSchedule = flightScheduleRes.Value);
        EntityHelper.Patch(record.BasePrice, v => ticket.BasePrice = v);
        EntityHelper.Patch(record.Taxes, v => ticket.Taxes = v);
        EntityHelper.Patch(record.IsRefundable, v => ticket.IsRefundable = v);
        EntityHelper.Patch(record.SeatInventory, v => ticket.SeatInventory = v);

        await UpdateAsync(ticket);
        return UnitResult.Success<Error>();
    }

    public async new Task<UnitResult<Error>> DeleteAsync(int id)
    {
        var result = await base.DeleteAsync(id);
        if (!result) return UnitResult.Failure(Error.NotFound(nameof(Flight), id));
        return UnitResult.Success<Error>();
    }
}