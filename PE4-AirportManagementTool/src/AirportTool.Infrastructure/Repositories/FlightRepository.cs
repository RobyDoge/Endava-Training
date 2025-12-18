using AirportTool.Application.Abstractions;
using AirportTool.Application.Records;
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

    public FlightRepository(AirlineBookingContext context, IMapper mapper) : base(context)
    {
        Context = context;
        Mapper = mapper;
    }

    public async Task<Result<IIdentifiable<int>, Error>> CreateAsync(CreateFlightRecord record)
    {
        var airlineRes = await Utils.FindRequiredEntity(Context.Airlines, nameof(Airline.Iatacode), record.AirlineIata);
        if (airlineRes.IsFailure) return airlineRes.ConvertFailure<IIdentifiable<int>>();

        var originRes = await Utils.FindRequiredEntity(Context.Airports, nameof(Airport.Iatacode), record.OriginAirportIata);
        if (originRes.IsFailure) return originRes.ConvertFailure<IIdentifiable<int>>();

        var destinationRes = await Utils.FindRequiredEntity(Context.Airports, nameof(Airport.Iatacode), record.DestinationAirportIata);
        if (destinationRes.IsFailure) return destinationRes.ConvertFailure<IIdentifiable<int>>();

        var defaultAircraftRes = await Utils.FindRequiredEntity(Context.Aircrafts, nameof(Aircraft.TailName), record.DefaultAircraftTailName);
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

        var airlineRes = await Utils.FindRequiredEntity(Context.Airlines, nameof(Airline.Iatacode), record.AirlineIata);
        if (airlineRes.IsFailure && record.AirlineIata != null) return airlineRes;

        var originRes = await Utils.FindRequiredEntity(Context.Airports, nameof(Airport.Iatacode), record.OriginAirportIata);
        if (originRes.IsFailure && record.OriginAirportIata != null) return originRes;

        var destinationRes = await Utils.FindRequiredEntity(Context.Airports, nameof(Airport.Iatacode), record.DestinationAirportIata);
        if (destinationRes.IsFailure && record.DestinationAirportIata != null) return destinationRes;

        var defaultAircraftRes = await Utils.FindRequiredEntity(Context.Aircrafts, nameof(Aircraft.TailName), record.DefaultAircraftTailName);
        if (defaultAircraftRes.IsFailure && record.DefaultAircraftTailName != null) return defaultAircraftRes;

        Utils.Patch(airlineRes.Value, a => flight.Airline = a);
        Utils.Patch(originRes.Value, a => flight.OriginAirport = a);
        Utils.Patch(destinationRes.Value, a => flight.DestinationAirport = a);
        Utils.Patch(defaultAircraftRes.Value, a => flight.DefaultAircraft = a);
        Utils.Patch(record.FlightNumber, v => flight.FlightNumber = v);
        Utils.Patch(record.IsActive, v => flight.IsActive = v);

        await UpdateAsync(flight);
        return UnitResult.Success<Error>();
    }

    public async new Task<UnitResult<Error>> DeleteAsync(int id)
    {
        var result = await base.DeleteAsync(id);
        if (!result) return UnitResult.Failure(Error.NotFound(nameof(Flight), id));
        return UnitResult.Success<Error>();
    }
}