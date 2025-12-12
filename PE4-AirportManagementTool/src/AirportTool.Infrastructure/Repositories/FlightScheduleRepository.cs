using AirportTool.Application.Abstractions;
using AirportTool.Domain.Entities;
using AirportTool.Infrastructure.Context;
using AirportTool.Infrastructure.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

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

    public async Task<IEnumerable<FlightScheduleEntity>> GetByRouteAndDateAsync(string fromIata, string toIata, DateTime date)
    {
        var dayStart = date.Date;
        var dayEnd = date.Date.AddDays(1);

        var result = await Context.FlightSchedules
            .Include(fs => fs.Gate)
            .Include(fs => fs.AssignedAircraft)
            .Include(fs => fs.Flight)
                .ThenInclude(f => f.DefaultAircraft)
            .Where(fs => fs.Flight.OriginAirport.Iatacode.ToUpper() == fromIata.ToUpper() &&
                         fs.Flight.DestinationAirport.Iatacode.ToUpper() == toIata.ToUpper() &&
                         fs.ScheduledDepartureUtc >= dayStart &&
                         fs.ScheduledDepartureUtc < dayEnd &&
                         fs.Flight.IsActive)
            .ToListAsync();

        return Mapper.Map<IEnumerable<FlightScheduleEntity>>(result);
    }
}