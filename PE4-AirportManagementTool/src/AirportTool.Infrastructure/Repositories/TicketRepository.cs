using AirportTool.Application.Abstractions;
using AirportTool.Domain.Entities;
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

    public async Task<Result<IEnumerable<TicketEntity>>> GetTicketsByFlightAsync(int flightId)
    {
        try
        {
            var result = await Context.Tickets
                .Include(t => t.FlightSchedule)
                .Where(t => t.FlightSchedule.FlightId == flightId)
                .ToListAsync();
            if (result == null || result.Count == 0) return Result.Failure<IEnumerable<TicketEntity>>("No tickets found for the specified flight.");

            return Result.Success(Mapper.Map<IEnumerable<TicketEntity>>(result));
        }
        catch (Exception ex)
        {
            return Result.Failure<IEnumerable<TicketEntity>>($"An error occurred while retrieving tickets: {ex.Message}");
        }
    }
}