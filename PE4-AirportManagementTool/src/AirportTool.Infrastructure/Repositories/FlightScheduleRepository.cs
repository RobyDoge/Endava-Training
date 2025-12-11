using AirportTool.Application.Abstractions;
using AirportTool.Domain.Entities;
using AirportTool.Infrastructure.Context;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace AirportTool.Infrastructure.Repositories;

public class FlightScheduleRepository : GenericRepository<FlightScheduleEntity>, IFlightScheduleRepository
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
            .Where(fs => fs.Flight.OriginAirport.Iatacode.ToUpper() == fromIata.ToUpper() &&
                         fs.Flight.DestinationAirport.Iatacode.ToUpper() == toIata.ToUpper() &&
                         fs.ScheduledDepartureUtc >= dayStart &&
                         fs.ScheduledDepartureUtc < dayEnd)
            .ToListAsync();
        return Mapper.Map<IEnumerable<FlightScheduleEntity>>(result);
    }
}