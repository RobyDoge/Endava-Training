using AirportTool.Application.Abstractions;
using AirportTool.Application.Records;
using AirportTool.Domain.Entities;
using AirportTool.Domain.Errors;
using AirportTool.Infrastructure.Context;
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

    public FlightScheduleRepository(AirlineBookingContext context, IMapper mapper) : base(context)
    {
        Context = context;
        Mapper = mapper;
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
}