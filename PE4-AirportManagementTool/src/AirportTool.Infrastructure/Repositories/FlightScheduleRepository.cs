
using AirportTool.Application.Abstractions;
using AirportTool.Domain.Entities;
using AirportTool.Infrastructure.Context;

namespace AirportTool.Infrastructure.Repositories;

public class FlightScheduleRepository : GenericRepository<FlightScheduleEntity>, IFlightScheduleRepository
{
    public FlightScheduleRepository(AirlineBookingContext context) : base(context)
    {
    }
}
