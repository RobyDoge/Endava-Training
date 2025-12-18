using AirportTool.Application.Records;
using AirportTool.Domain.Entities;
using AirportTool.Domain.Errors;
using CSharpFunctionalExtensions;

namespace AirportTool.Application.Abstractions;

public interface ITicketRepository
{
    Task<Result<IEnumerable<TicketEntity>, Error>> GetTicketsByFlightAsync(int flightId);

    Task<Result<IIdentifiable<int>, Error>> CreateAsync(CreateTicketRecord record);
}