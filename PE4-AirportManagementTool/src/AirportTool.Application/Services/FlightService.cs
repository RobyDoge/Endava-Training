using AirportTool.Application.Abstractions;
using AirportTool.Application.Records;
using AirportTool.Application.Validators;
using AirportTool.Domain.Entities;
using AirportTool.Domain.Errors;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Xml.XPath;

namespace AirportTool.Application.Services;

public class FlightService
{
    private IUnitOfWork UnitOfWork { get; }
    private IServiceProvider ServiceProvider { get; }

    public FlightService(IUnitOfWork unitOfWork, IServiceProvider serviceProvider)
    {
        UnitOfWork = unitOfWork;
        ServiceProvider = serviceProvider;
    }

    public async Task<Result<int, Error>> CreateAsync(CreateFlightRecord createFlightRecord)
    {
        var validator = ServiceProvider.GetRequiredService<IValidator<CreateFlightRecord>>()
            ?? throw new InvalidOperationException("CreateFlightValidator not registered in the service provider.");

        var res = await validator.ValidateAsync(createFlightRecord);
        if (!res.IsValid)
        {
            var errors = string.Join("; ", res.Errors);
            return Result.Failure<int, Error>(Error.Validation(errors));
        }

        var result = await UnitOfWork.FlightRepository.CreateAsync(createFlightRecord);

        await UnitOfWork.SaveChangesAsync();

        if (result.IsFailure) return Result.Failure<int, Error>(result.Error);

        return Result.Success<int, Error>(result.Value.Id);
    }

    public async Task<UnitResult<Error>> UpdateAsync(int id,
            UpdateFlightRecord record)
    {
        var validator = ServiceProvider.GetRequiredService<IValidator<UpdateFlightRecord>>()
            ?? throw new InvalidOperationException("UpdateFlightValidator not registered in the service provider.");

        var res = await validator.ValidateAsync(record);
        if (!res.IsValid)
        {
            var errors = string.Join("; ", res.Errors);
            return UnitResult.Failure<Error>(Error.Validation(errors));
        }

        var result = await UnitOfWork.FlightRepository.UpdateAsync(id, record);

        await UnitOfWork.SaveChangesAsync();

        return result;
    }

    public async Task<UnitResult<Error>> DeleteAsync(int id)
    {
        var flight = await UnitOfWork.FlightRepository.DeleteAsync(id);
        await UnitOfWork.SaveChangesAsync();
        return flight;
    }
}