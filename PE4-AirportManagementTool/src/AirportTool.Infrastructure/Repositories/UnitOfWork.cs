using AirportTool.Application.Abstractions;
using AirportTool.Infrastructure.Context;

namespace AirportTool.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private AirlineBookingContext Context { get; }
    public IFlightScheduleRepository FlightScheduleRepository { get; }
    public IFlightRepository FlightRepository { get; }
    public ITicketRepository TicketRepository { get; }

    public UnitOfWork(AirlineBookingContext context,
        IFlightScheduleRepository flightScheduleRepository,
        IFlightRepository flightRepository,
        ITicketRepository ticketRepository)
    {
        Context = context;
        FlightScheduleRepository = flightScheduleRepository;
        FlightRepository = flightRepository;
        TicketRepository = ticketRepository;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => Context.SaveChangesAsync(cancellationToken);

    public void Dispose()
    {
        Context.Dispose();
    }
}