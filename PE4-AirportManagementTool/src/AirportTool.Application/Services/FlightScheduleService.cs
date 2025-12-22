using AirportTool.Application.Abstractions;
using AirportTool.Application.Records;
using AirportTool.Domain.Entities;
using AirportTool.Domain.Errors;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Formats.Asn1;

namespace AirportTool.Application.Services;

public class FlightScheduleService
{
    private IUnitOfWork UnitOfWork { get; }
    public IServiceProvider ServiceProvider { get; }

    public FlightScheduleService(IUnitOfWork unitOfWork, IServiceProvider serviceProvider)
    {
        UnitOfWork = unitOfWork;
        ServiceProvider = serviceProvider;
    }

    public async Task<Result<IEnumerable<FlightScheduleEntity>, Error>> GetByRouteAndDate(GetFlightsByRouteAndDateRecord record)
    {
        var result = await UnitOfWork.FlightScheduleRepository.GetByRouteAndDateAsync(record);
        return result;
    }

    public async Task<Result<FlightScheduleEntity, Error>> GetById(int id)
    {
        var result = await UnitOfWork.FlightScheduleRepository.GetByIdAsync(id);
        return result;
    }

    public async Task<Result<IEnumerable<FlightScheduleEntity>, Error>> GetUpcoming(DateTime starDate, DateTime endDate)
    {
        if (starDate >= endDate)
        {
            return Result.Failure<IEnumerable<FlightScheduleEntity>, Error>(Error.Validation("Start date must be earlier than end date."));
        }

        var result = await UnitOfWork.FlightScheduleRepository.GetUpcomingAsync(starDate, endDate);
        return result;
    }

    public async Task<Result<int, Error>> CreateAsync(CreateFlightScheduleRecord record)
    {
        var validator = ServiceProvider.GetRequiredService<IValidator<CreateFlightScheduleRecord>>()
            ?? throw new InvalidOperationException("CreateFlightScheduleRecord not registered in the service provider.");

        var validationResult = await validator.ValidateAsync(record);
        if (!validationResult.IsValid)
        {
            var errorMessage = string.Join("; ", validationResult.Errors);
            return Result.Failure<int, Error>(Error.Validation(errorMessage));
        }

        var result = await UnitOfWork.FlightScheduleRepository.AddAsync(record);
        if (result.IsFailure) return result.ConvertFailure<int>();

        await UnitOfWork.SaveChangesAsync();

        return result.Value.Id;
    }
}