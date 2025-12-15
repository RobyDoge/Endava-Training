using AirportTool.Application.Abstractions;
using AirportTool.Infrastructure.Context;
using AirportTool.Infrastructure.Identifiable;
using AirportTool.Infrastructure.Models;
using CSharpFunctionalExtensions;
using AutoMapper;
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
            var airline = await Context.Airlines.SingleAsync(a => a.Iatacode == airlineIata);
            var origin = await Context.Airports.SingleAsync(a => a.Iatacode == originIata);
            var destination = await Context.Airports.SingleAsync(a => a.Iatacode == destinationIata);
            var defaultAircraft = await Context.Aircrafts.SingleOrDefaultAsync(a => a.TailName == defaultAircraftTailName);
            var flight = new Flight
            {
                FlightNumber = flightNumber,
                Airline = airline,
                OriginAirport = origin,
                DestinationAirport = destination,
                DefaultAircraft = defaultAircraft,
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

    public async Task<Result> UpdateAsync(int id, string? airlineIata, string? flightNumber, string? originIata, string? destinationIata, string? defaultAircraftTailName, bool isActive)
    {
        try
        {
            var airline = await Context.Airlines.SingleOrDefaultAsync(a => a.Iatacode == airlineIata);
            var origin = await Context.Airports.SingleOrDefaultAsync(a => a.Iatacode == originIata);
            var destination = await Context.Airports.SingleOrDefaultAsync(a => a.Iatacode == destinationIata);
            var defaultAircraft = await Context.Aircrafts.SingleOrDefaultAsync(a => a.TailName == defaultAircraftTailName);

            if (airline == null)
            {
                if (airlineIata != null) return Result.Failure($"Airline with IATA code '{airlineIata}' not found.");

                airline = await Context.Airlines.SingleAsync(a => a.Flights.Any(f => f.Id == id));
            }
            if (origin == null)
            {
                if (originIata != null) return Result.Failure($"Origin airport with IATA code '{originIata}' not found.");

                origin = await Context.Airports.SingleAsync(a => a.FlightOriginAirports.Any(f => f.Id == id && f.OriginAirportId == a.Id));
            }
            if (destination == null)
            {
                if (destinationIata != null) return Result.Failure($"Destination airport with IATA code '{destinationIata}' not found.");
                destination = await Context.Airports.SingleAsync(a => a.FlightDestinationAirports.Any(f => f.Id == id && f.DestinationAirportId == a.Id));
            }
            if (defaultAircraft == null)
            {
                if (defaultAircraftTailName != null) return Result.Failure($"Default aircraft with tail name '{defaultAircraftTailName}' not found.");
                defaultAircraft = await Context.Aircrafts.SingleOrDefaultAsync(a => a.Flights.Any(f => f.Id == id && f.DefaultAircraftId == a.Id));
            }
            if (flightNumber == null)
            {
                var existingFlight = await Context.Flights.AsNoTracking().SingleAsync(f => f.Id == id);
                flightNumber = existingFlight.FlightNumber;
            }

            var updatedFlight = new Flight
            {
                Id = id,
                FlightNumber = flightNumber,
                Airline = airline,
                OriginAirport = origin,
                DestinationAirport = destination,
                DefaultAircraft = defaultAircraft,
                IsActive = isActive
            };

            await UpdateAsync(updatedFlight);
            return Result.Success();
        }
        catch (Exception e)
        {
            return Result.Failure(e.Message);
        }
    }
}