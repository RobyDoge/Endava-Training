using AirportTool.Application.Abstractions;
using AirportTool.Application.Records;
using AirportTool.Domain.Entities;
using AirportTool.Domain.Enums;
using AirportTool.Domain.Errors;
using AirportTool.Infrastructure.Context;
using AirportTool.Infrastructure.Identifiable;
using AirportTool.Infrastructure.Models;
using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Collections.Generic;

namespace AirportTool.Infrastructure.Repositories;

public class FlightScheduleRepository : GenericRepository<FlightSchedule>, IFlightScheduleRepository
{
    private AirlineBookingContext Context { get; }
    private IMapper Mapper { get; }
    public IEntityHelper EntityHelper { get; }

    public FlightScheduleRepository(AirlineBookingContext context, IMapper mapper, IEntityHelper entityHelper) : base(context, mapper)
    {
        Context = context;
        Mapper = mapper;
        EntityHelper = entityHelper;
    }

    public async Task<Result<IEnumerable<FlightScheduleEntity>, Error>> GetByRouteAndDateAsync(GetFlightsByRouteAndDateRecord record)
    {
        var dayStart = record.Date.Date;
        var dayEnd = record.Date.Date.AddDays(1);

        var result = await Context.FlightSchedules
            .Include(fs => fs.Gate)
            .Include(fs => fs.AssignedAircraft)
            .Include(fs => fs.Flight)
            .ThenInclude(f => f.DefaultAircraft)
            .Where(fs => fs.Flight.OriginAirport.Iatacode.ToUpper() == record.FromIata.ToUpper() &&
                            fs.Flight.DestinationAirport.Iatacode.ToUpper() == record.ToIata.ToUpper() &&
                            fs.ScheduledDepartureUtc >= dayStart &&
                            fs.ScheduledDepartureUtc < dayEnd &&
                            fs.Flight.IsActive)
            .ToListAsync();

        return Result.Success<IEnumerable<FlightScheduleEntity>, Error>(Mapper.Map<IEnumerable<FlightScheduleEntity>>(result));
    }

    public async Task<Result<IEnumerable<FlightScheduleEntity>, Error>> GetUpcomingAsync(DateTime startingDate, DateTime endingDate)
    {
        var result = await Context.FlightSchedules
            .Include(fs => fs.Gate)
            .Include(fs => fs.AssignedAircraft)
            .Include(fs => fs.Flight)
            .ThenInclude(f => f.DefaultAircraft)
            .Where(fs => fs.ScheduledDepartureUtc >= startingDate &&
                            fs.ScheduledDepartureUtc <= endingDate)
            .ToListAsync();

        return Result.Success<IEnumerable<FlightScheduleEntity>, Error>(Mapper.Map<IEnumerable<FlightScheduleEntity>>(result));
    }

    public async new Task<Result<IIdentifiable<int>, Error>> AddAsync(CreateFlightScheduleRecord record)
    {
        var flightRes = await EntityHelper.FindEntityAsync(Context.Flights, nameof(Flight.Id), record.FlightId);
        if (flightRes.IsFailure) return flightRes.ConvertFailure<IIdentifiable<int>>();

        var gate = await Context.Gates
            .Where(g => g.AirportId == flightRes.Value.OriginAirportId
                && g.Code.ToLower() == record.GateCode.ToLower())
            .FirstOrDefaultAsync();
        if (gate is null) return Result.Failure<IIdentifiable<int>, Error>(Error.NotFound(nameof(Gate), record.GateCode));

        var aircraft = await EntityHelper.FindEntityAsync(Context.Aircrafts, nameof(Aircraft.TailName), record.AssignedAircraftTailName);
        if (aircraft.IsFailure) return aircraft.ConvertFailure<IIdentifiable<int>>();

        var newFlightSchedule = new FlightSchedule
        {
            Flight = flightRes.Value,
            Gate = gate,
            AssignedAircraft = aircraft.Value,
            ScheduledArrivalUtc = record.ScheduledArrivalUtc,
            ScheduledDepartureUtc = record.ScheduledDepartureUtc,
            FlightScheduleStatusId = (int)FlightScheduleStatusEnum.Planned
        };

        var result = await AddAsync(newFlightSchedule);
        IIdentifiable<int> id = new Identifiable<int>(result, nameof(result.Id));
        return Result.Success<IIdentifiable<int>, Error>(id);
    }

    public Task<Result<BulkImportFlightScheduleSummary>> BulkImport(IEnumerable<CreateFlightScheduleRecord> records)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<FlightScheduleEntity, Error>> GetByIdAsync(int flightId)
    {
        var flightSchedule = await Context.FlightSchedules
            .Include(fs => fs.Gate)
            .Include(fs => fs.AssignedAircraft)
            .Include(fs => fs.Flight)
            .ThenInclude(f => f.DefaultAircraft)
            .FirstOrDefaultAsync(fs => fs.Id == flightId);

        if (flightSchedule == null) return Result.Failure<FlightScheduleEntity, Error>(Error.NotFound("FlightSchedule", flightId));

        return Result.Success<FlightScheduleEntity, Error>(Mapper.Map<FlightScheduleEntity>(flightSchedule));
    }

    public async Task<Result<bool, Error>> UpsertAsync(UpsertFlightScheduleRecord record)
    {
        if (record.Id == null)
        {
            var addResult = await this.AddAsync(Mapper.Map<CreateFlightScheduleRecord>(record));
            if (addResult.IsFailure) addResult.ConvertFailure<bool>();
            return true;
        }

        var result = await this.UpdateAsync(Mapper.Map<UpdateFlightScheduleRecord>(record));
        if (result.IsFailure) result.ConvertFailure<bool>();
        return false;
    }

    public async Task<UnitResult<Error>> UpdateAsync(UpdateFlightScheduleRecord record)
    {
        var flightSchedule = await Context.FlightSchedules
            .Include(fs => fs.Gate)
            .Include(fs => fs.AssignedAircraft)
            .Include(fs => fs.Flight)
            .ThenInclude(f => f.DefaultAircraft)
            .Where(fs => fs.Id == record.Id)
            .SingleOrDefaultAsync();
        if (flightSchedule == null) return Error.NotFound(nameof(FlightSchedule), record.Id);

        var flightRes = await EntityHelper.FindEntityAsync(Context.Flights, nameof(Flight.Id), record.FlightId);
        if (flightRes.IsFailure && record.FlightId != null) return flightRes;

        var gate = await Context.Gates
            .Where(g => g.AirportId == flightRes.Value.OriginAirportId
                && g.Code.ToLower() == record.GateCode.ToLower())
            .FirstOrDefaultAsync();
        if (gate is null && record.GateCode != null) return Result.Failure<IIdentifiable<int>, Error>(Error.NotFound(nameof(Gate), record.GateCode));

        var aircraft = await EntityHelper.FindEntityAsync(Context.Aircrafts, nameof(Aircraft.TailName), record.AssignedAircraftTailName);
        if (aircraft.IsFailure && record.AssignedAircraftTailName != null) return aircraft.ConvertFailure<IIdentifiable<int>>();

        EntityHelper.Patch(flightRes.Value, a => flightSchedule.Flight = a);
        EntityHelper.Patch(gate, a => flightSchedule.Gate = a);
        EntityHelper.Patch(aircraft.Value, a => flightSchedule.AssignedAircraft = a);
        EntityHelper.Patch(record.ScheduledDepartureUtc, a => flightSchedule.ScheduledDepartureUtc = a);
        EntityHelper.Patch(record.ScheduledArrivalUtc, a => flightSchedule.ScheduledArrivalUtc = a);

        await UpdateAsync(flightSchedule);
        return UnitResult.Success<Error>();
    }
}