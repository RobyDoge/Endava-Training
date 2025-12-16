using AirportTool.Application.Abstractions;
using AirportTool.Domain.Entities;
using AirportTool.Domain.Errors;
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

    public async Task<Result<IEnumerable<TicketEntity>, Error>> GetTicketsByFlight(int flightId)
    {
        var aux = await UnitOfWork.TicketRepository.GetTicketsByFlightAsync(flightId);
        return aux;
    }
}