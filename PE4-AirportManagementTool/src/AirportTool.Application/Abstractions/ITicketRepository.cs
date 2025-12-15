using AirportTool.Domain.Entities;
using CSharpFunctionalExtensions;

namespace AirportTool.Application.Abstractions;

public interface ITicketRepository
{
    Task<Result<IEnumerable<TicketEntity>>> GetTicketsByFlightAsync(int flightId);
}