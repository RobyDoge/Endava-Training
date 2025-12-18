using AirportTool.Application.Abstractions;
using AirportTool.Application.Records;
using AirportTool.Domain.Entities;
using AirportTool.Domain.Errors;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Text;

namespace AirportTool.Application.Services;

public class TicketsService
{
    private IUnitOfWork UnitOfWork { get; }
    public IServiceProvider ServiceProvider { get; }

    public TicketsService(IUnitOfWork unitOfWork, IServiceProvider serviceProvider)
    {
        UnitOfWork = unitOfWork;
        ServiceProvider = serviceProvider;
    }

    public async Task<Result<IEnumerable<TicketEntity>, Error>> GetTicketsByFlight(int flightId)
    {
        var aux = await UnitOfWork.TicketRepository.GetTicketsByFlightAsync(flightId);
        return aux;
    }

    public async Task<Result<int, Error>> CreateTicket(CreateTicketRecord record)
    {
        var validator = ServiceProvider.GetRequiredService<IValidator<CreateTicketRecord>>()
            ?? throw new InvalidOperationException("CreateTicketValidator not registered in the service provider.");

        var res = await validator.ValidateAsync(record);
        if (!res.IsValid)
        {
            var errors = string.Join("; ", res.Errors);
            return Result.Failure<int, Error>(Error.Validation(errors));
        }

        var result = await UnitOfWork.TicketRepository.CreateAsync(record);

        await UnitOfWork.SaveChangesAsync();

        if (result.IsFailure) return Result.Failure<int, Error>(result.Error);

        return Result.Success<int, Error>(result.Value.Id);
    }
}