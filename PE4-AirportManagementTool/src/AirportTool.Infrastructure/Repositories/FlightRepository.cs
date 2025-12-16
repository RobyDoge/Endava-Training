using AirportTool.Application.Abstractions;
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

    public async Task<Result<IIdentifiable<int>, Error>> CreateAsync(string airlineIata, string flightNumber, string originIata, string destinationIata, string? defaultAircraftTailName)
    {
        var airlineRes = await Utils.FindByPropertyString(Context.Airlines, nameof(Airline.Iatacode), airlineIata);
        if (airlineRes.IsFailure) return Result.Failure<IIdentifiable<int>, Error>(Error.NotFound(nameof(Airline), airlineIata));

        var originRes = await Utils.FindByPropertyString(Context.Airports, nameof(Airport.Iatacode), originIata);
        if (originRes.IsFailure) return Result.Failure<IIdentifiable<int>, Error>(Error.NotFound(nameof(Airport), originIata));

        var destinationRes = await Utils.FindByPropertyString(Context.Airports, nameof(Airport.Iatacode), destinationIata);
        if (destinationRes.IsFailure) return Result.Failure<IIdentifiable<int>, Error>(Error.NotFound(nameof(Airport), destinationIata));

        var defaultAircraftRes = await Utils.FindByPropertyString(Context.Aircrafts, nameof(Aircraft.TailName), defaultAircraftTailName);
        if (defaultAircraftRes.IsFailure) return Result.Failure<IIdentifiable<int>, Error>(Error.NotFound(nameof(Aircraft), defaultAircraftTailName!));

        var flight = new Flight
        {
            FlightNumber = flightNumber,
            Airline = airlineRes.Value,
            OriginAirport = originRes.Value,
            DestinationAirport = destinationRes.Value,
            DefaultAircraft = defaultAircraftRes.Value,
            IsActive = true
        };

        var result = await AddAsync(flight);
        IIdentifiable<int> id = new Identifiable<int>(result, nameof(flight.Id));
        return Result.Success<IIdentifiable<int>, Error>(id);
    }

    public async Task<UnitResult<Error>> UpdateAsync(int id, string? airlineIata, string? flightNumber, string? originIata, string? destinationIata, string? defaultAircraftTailName, bool? isActive)
    {
        var flight = await Context.Flights
            .Include(f => f.Airline)
            .Include(f => f.OriginAirport)
            .Include(f => f.DestinationAirport)
            .Include(f => f.DefaultAircraft)
            .SingleOrDefaultAsync(f => f.Id == id);
        if (flight == null) return UnitResult.Failure(Error.NotFound(nameof(Flight), id));

        var airlineRes = await Utils.FindByPropertyString(Context.Airlines, nameof(Airline.Iatacode), airlineIata);
        if (airlineRes.IsFailure && airlineIata != null) return UnitResult.Failure(Error.NotFound(nameof(Airline), airlineIata));

        var originRes = await Utils.FindByPropertyString(Context.Airports, nameof(Airport.Iatacode), originIata);
        if (originRes.IsFailure && originIata != null) return UnitResult.Failure(Error.NotFound(nameof(Airport), originIata));

        var destinationRes = await Utils.FindByPropertyString(Context.Airports, nameof(Airport.Iatacode), destinationIata);
        if (destinationRes.IsFailure && destinationIata != null) return UnitResult.Failure(Error.NotFound(nameof(Airport), destinationIata));

        var defaultAircraftRes = await Utils.FindByPropertyString(Context.Aircrafts, nameof(Aircraft.TailName), defaultAircraftTailName);
        if (defaultAircraftRes.IsFailure && defaultAircraftTailName != null) return UnitResult.Failure(Error.NotFound(nameof(Aircraft), defaultAircraftTailName));

        Utils.Patch(airlineRes.Value, a => flight.Airline = a);
        Utils.Patch(originRes.Value, a => flight.OriginAirport = a);
        Utils.Patch(destinationRes.Value, a => flight.DestinationAirport = a);
        Utils.Patch(defaultAircraftRes.Value, a => flight.DefaultAircraft = a);
        Utils.Patch(flightNumber, v => flight.FlightNumber = v);
        Utils.Patch(isActive, v => flight.IsActive = v);

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