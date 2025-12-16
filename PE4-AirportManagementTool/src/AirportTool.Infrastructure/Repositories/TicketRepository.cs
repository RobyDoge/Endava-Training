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
            .Where(t => t.FlightSchedule.FlightId == flightId)
            .ToListAsync();
        IEnumerable<TicketEntity> aux = [];
        foreach (var ticket in result)
        {
            Console.WriteLine($"Ticket {ticket.Id}: CurrencyId={ticket.CurrencyId}, FareClassId={ticket.FareClassId}");
            var aux2 = Mapper.Map<TicketEntity>(ticket);
            Console.WriteLine($"{aux2.Currency.ToString()} , {aux2.FareClass.ToString()}");
            aux = aux.Append(aux2);
        }

        return Result.Success<IEnumerable<TicketEntity>, Error>(aux);
    }
}