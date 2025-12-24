using AirportTool.Application.Abstractions;
using AirportTool.Application.Records;
using AirportTool.Domain.Entities;
using AirportTool.Domain.Errors;
using AirportTool.Infrastructure.Context;
using AirportTool.Infrastructure.Identifiable;
using AirportTool.Infrastructure.Models;
using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace AirportTool.Infrastructure.Repositories;

public class FlightRepository : GenericRepository<Flight>, IFlightRepository
{
    private AirlineBookingContext Context { get; }
    private IMapper Mapper { get; }
    public IEntityHelper EntityHelper { get; }

    public FlightRepository(AirlineBookingContext context, IMapper mapper, IEntityHelper entityHelper) : base(context, mapper)
    {
        Context = context;
        Mapper = mapper;
        EntityHelper = entityHelper;
    }

    public async Task<Result<IIdentifiable<int>, Error>> CreateAsync(CreateFlightRecord record)
    {
        var airlineRes = await EntityHelper.FindEntityAsync(Context.Airlines, nameof(Airline.Iatacode), record.AirlineIata);
        if (airlineRes.IsFailure) return airlineRes.ConvertFailure<IIdentifiable<int>>();

        var originRes = await EntityHelper.FindEntityAsync(Context.Airports, nameof(Airport.Iatacode), record.OriginAirportIata);
        if (originRes.IsFailure) return originRes.ConvertFailure<IIdentifiable<int>>();

        var destinationRes = await EntityHelper.FindEntityAsync(Context.Airports, nameof(Airport.Iatacode), record.DestinationAirportIata);
        if (destinationRes.IsFailure) return destinationRes.ConvertFailure<IIdentifiable<int>>();

        var defaultAircraftRes = await EntityHelper.FindEntityAsync(Context.Aircrafts, nameof(Aircraft.TailName), record.DefaultAircraftTailName);
        if (defaultAircraftRes.IsFailure) return defaultAircraftRes.ConvertFailure<IIdentifiable<int>>();

        var flight = new Flight
        {
            FlightNumber = record.FlightNumber,
            Airline = airlineRes.Value,
            OriginAirport = originRes.Value,
            DestinationAirport = destinationRes.Value,
            DefaultAircraft = defaultAircraftRes.Value
        };

        var result = await AddAsync(flight);
        IIdentifiable<int> id = new Identifiable<int>(result, nameof(flight.Id));
        return Result.Success<IIdentifiable<int>, Error>(id);
    }

    public async Task<UnitResult<Error>> UpdateAsync(int id, UpdateFlightRecord record)
    {
        var flight = await Context.Flights
            .Include(f => f.Airline)
            .Include(f => f.OriginAirport)
            .Include(f => f.DestinationAirport)
            .Include(f => f.DefaultAircraft)
            .SingleOrDefaultAsync(f => f.Id == id);
        if (flight == null) return UnitResult.Failure(Error.NotFound(nameof(Flight), id));

        var airlineRes = await EntityHelper.FindEntityAsync(Context.Airlines, nameof(Airline.Iatacode), record.AirlineIata);
        if (airlineRes.IsFailure && record.AirlineIata != null) return airlineRes;

        var originRes = await EntityHelper.FindEntityAsync(Context.Airports, nameof(Airport.Iatacode), record.OriginAirportIata);
        if (originRes.IsFailure && record.OriginAirportIata != null) return originRes;

        var destinationRes = await EntityHelper.FindEntityAsync(Context.Airports, nameof(Airport.Iatacode), record.DestinationAirportIata);
        if (destinationRes.IsFailure && record.DestinationAirportIata != null) return destinationRes;

        var defaultAircraftRes = await EntityHelper.FindEntityAsync(Context.Aircrafts, nameof(Aircraft.TailName), record.DefaultAircraftTailName);
        if (defaultAircraftRes.IsFailure && record.DefaultAircraftTailName != null) return defaultAircraftRes;

        EntityHelper.Patch(airlineRes.Value, a => flight.Airline = a);
        EntityHelper.Patch(originRes.Value, a => flight.OriginAirport = a);
        EntityHelper.Patch(destinationRes.Value, a => flight.DestinationAirport = a);
        EntityHelper.Patch(defaultAircraftRes.Value, a => flight.DefaultAircraft = a);
        EntityHelper.Patch(record.FlightNumber, v => flight.FlightNumber = v);
        EntityHelper.Patch(record.IsActive, v => flight.IsActive = v);

        await UpdateAsync(flight);
        return UnitResult.Success<Error>();
    }

    public async new Task<UnitResult<Error>> DeleteAsync(int id)
    {
        var result = await base.DeleteAsync(id);
        if (!result) return UnitResult.Failure(Error.NotFound(nameof(Flight), id));
        return UnitResult.Success<Error>();
    }

    public async Task<Result<FlightEntity, Error>> GetAsync(int id)
    {
        var flight = await Context.Flights
            .Include(f => f.Airline)
            .Include(f => f.OriginAirport)
            .Include(f => f.DestinationAirport)
            .Include(f => f.DefaultAircraft)
            .SingleOrDefaultAsync(f => f.Id == id);
        if (flight == null) return Result.Failure<FlightEntity, Error>(Error.NotFound(nameof(Flight), id));

        return Mapper.Map<FlightEntity>(flight);
    }
}