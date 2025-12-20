using AirportTool.Application.Abstractions;
using AirportTool.Infrastructure.Context;

namespace AirportTool.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private AirlineBookingContext Context { get; }
    public IFlightScheduleRepository FlightScheduleRepository { get; }
    public IFlightRepository FlightRepository { get; }
    public ITicketRepository TicketRepository { get; }
    public IBookingRepository BookingRepository { get; }

    public UnitOfWork(AirlineBookingContext context,
        IFlightScheduleRepository flightScheduleRepository,
        IFlightRepository flightRepository,
        ITicketRepository ticketRepository,
        IBookingRepository bookingRepository)
    {
        Context = context;
        FlightScheduleRepository = flightScheduleRepository;
        FlightRepository = flightRepository;
        TicketRepository = ticketRepository;
        BookingRepository = bookingRepository;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => Context.SaveChangesAsync(cancellationToken);

    public void Dispose()
    {
        Context.Dispose();
    }
}