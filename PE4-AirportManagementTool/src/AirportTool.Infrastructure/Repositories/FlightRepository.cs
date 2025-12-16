using AirportTool.Application.Abstractions;
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

    public async Task<Result<IIdentifiable<int>>> CreateAsync(string airlineIata, string flightNumber, string originIata, string destinationIata, string? defaultAircraftTailName)
    {
        try
        {
            var airlineRes = await Utils.FindByPropertyString(Context.Airlines, nameof(Airline.Iatacode), airlineIata);
            if (airlineRes.IsFailure) return Result.Failure<IIdentifiable<int>>(airlineRes.Error);

            var originRes = await Utils.FindByPropertyString(Context.Airports, nameof(Airport.Iatacode), originIata);
            if (originRes.IsFailure && originIata != null) return Result.Failure<IIdentifiable<int>>(originRes.Error);

            var destinationRes = await Utils.FindByPropertyString(Context.Airports, nameof(Airport.Iatacode), destinationIata);
            if (destinationRes.IsFailure && destinationIata != null) return Result.Failure<IIdentifiable<int>>(destinationRes.Error);

            var defaultAircraftRes = await Utils.FindByPropertyString(Context.Aircrafts, nameof(Aircraft.TailName), defaultAircraftTailName);
            if (defaultAircraftRes.IsFailure && defaultAircraftTailName != null) return Result.Failure<IIdentifiable<int>>(defaultAircraftRes.Error);

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
            return Result.Success(id);
        }
        catch (Exception e)
        {
            return Result.Failure<IIdentifiable<int>>(e.Message);
        }
    }

    public async Task<Result> UpdateAsync(int id, string? airlineIata, string? flightNumber, string? originIata, string? destinationIata, string? defaultAircraftTailName, bool? isActive)
    {
        try
        {
            var flight = await Context.Flights
                .Include(f => f.Airline)
                .Include(f => f.OriginAirport)
                .Include(f => f.DestinationAirport)
                .Include(f => f.DefaultAircraft)
                .SingleOrDefaultAsync(f => f.Id == id);
            if (flight == null) return Result.Failure("Flight not found.");

            var airlineRes = await Utils.FindByPropertyString(Context.Airlines, nameof(Airline.Iatacode), airlineIata);
            if (airlineRes.IsFailure && airlineIata != null) return Result.Failure(airlineRes.Error);

            var originRes = await Utils.FindByPropertyString(Context.Airports, nameof(Airport.Iatacode), originIata);
            if (originRes.IsFailure && originIata != null) return Result.Failure(originRes.Error);

            var destinationRes = await Utils.FindByPropertyString(Context.Airports, nameof(Airport.Iatacode), destinationIata);
            if (destinationRes.IsFailure && destinationIata != null) return Result.Failure(destinationRes.Error);

            var defaultAircraftRes = await Utils.FindByPropertyString(Context.Aircrafts, nameof(Aircraft.TailName), defaultAircraftTailName);
            if (defaultAircraftRes.IsFailure && defaultAircraftTailName != null) return Result.Failure(defaultAircraftRes.Error);

            Utils.Patch(airlineRes.Value, a => flight.Airline = a);
            Utils.Patch(originRes.Value, a => flight.OriginAirport = a);
            Utils.Patch(destinationRes.Value, a => flight.DestinationAirport = a);
            Utils.Patch(defaultAircraftRes.Value, a => flight.DefaultAircraft = a);
            Utils.Patch(flightNumber, v => flight.FlightNumber = v);
            Utils.Patch(isActive, v => flight.IsActive = v);

            await UpdateAsync(flight);
            return Result.Success();
        }
        catch (Exception e)
        {
            return Result.Failure(e.Message);
        }
    }

    public async new Task<Result> DeleteAsync(int id)
    {
        try
        {
            var result = await base.DeleteAsync(id);
            if (!result) return Result.Failure("Flight not found.");
            return Result.Success();
        }
        catch (Exception e)
        {
            return Result.Failure($"Error deleting flight: {e.Message}");
        }
    }
}