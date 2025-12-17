using AirportTool.Application.Abstractions;
using AirportTool.Domain.Entities;
using AirportTool.Domain.Errors;
using AirportTool.Infrastructure.Context;
using AirportTool.Infrastructure.Models;
using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace AirportTool.Infrastructure.Repositories;

public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
{
    private AirlineBookingContext Context { get; }
    private IMapper Mapper { get; }

    public TicketRepository(IMapper mapper, AirlineBookingContext context) : base(context)
    {
        Mapper = mapper;
        Context = context;
    }

    public async Task<Result<IEnumerable<TicketEntity>, Error>> GetTicketsByFlightAsync(int flightId)
    {
        var result = await Context.Tickets
            .Include(t => t.FlightSchedule)
            .Include(t => t.FareClass)
            .Include(t => t.Currency)
            .Where(t => t.FlightSchedule.FlightId == flightId)
            .ToListAsync();

        return Result.Success<IEnumerable<TicketEntity>, Error>(Mapper.Map<IEnumerable<TicketEntity>>(result));
    }
}