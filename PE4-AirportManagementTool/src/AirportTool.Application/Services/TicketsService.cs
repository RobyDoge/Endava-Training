using AirportTool.Application.Abstractions;
using AirportTool.Domain.Entities;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirportTool.Application.Services;

public class TicketsService
{
    private IUnitOfWork UnitOfWork { get; }

    public TicketsService(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<TicketEntity>>> GetTicketsByFlight(int flightId)
    {
        try
        {
            var tickets = await UnitOfWork.TicketRepository.GetTicketsByFlightAsync(flightId);
            return tickets;
        }
        catch (Exception ex)
        {
            return Result.Failure<IEnumerable<Domain.Entities.TicketEntity>>($"An error occurred while retrieving tickets: {ex.Message}");
        }
    }
}