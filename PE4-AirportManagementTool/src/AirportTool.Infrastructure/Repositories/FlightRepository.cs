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
}