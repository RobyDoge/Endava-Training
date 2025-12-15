using AirportTool.Application.Abstractions;
using AirportTool.Domain.Entities;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace AirportTool.Application.Services;

public class FlightService
{
    private IUnitOfWork UnitOfWork { get; }

    public FlightService(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    public async Task<Result<int>> CreateAsync(string airlineIata,
        string flightNumber,
        string originIata,
        string destinationIata,
        string? defaultAircraftTailName)
    {
        if (!Validators.FlightValidator.IsValidFlightNumber(flightNumber)) return Result.Failure<int>("Invalid Flight Number");
        if (!Validators.FlightValidator.AreAirportsDifferent(originIata, destinationIata)) return Result.Failure<int>("Airports must be different");

        var result = await UnitOfWork.FlightRepository.CreateAsync(
        airlineIata,
        flightNumber,
        originIata,
        destinationIata,
        defaultAircraftTailName);

        try
        {
            await UnitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return Result.Failure<int>($"Error saving flight: {ex.Message}");
        }

        if (result.IsFailure) return result.ConvertFailure<int>();
        return Result.Success(result.Value.Id);
    }

    public async Task<Result> UpdateAsync(int id,
        string? airlineIata,
        string? flightNumber,
        string? originIata,
        string? destinationIata,
        string? defaultAircraftTailName,
        bool isActive)
    {
        if (flightNumber != null && !Validators.FlightValidator.IsValidFlightNumber(flightNumber)) return Result.Failure("Invalid Flight Number");
        if (originIata != null && destinationIata != null && !Validators.FlightValidator.AreAirportsDifferent(originIata, destinationIata)) return Result.Failure("Airports must be different");

        var result = await UnitOfWork.FlightRepository.UpdateAsync(
        id,
        airlineIata,
        flightNumber,
        originIata,
        destinationIata,
        defaultAircraftTailName,
        isActive);
        try
        {
            await UnitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Error saving flight: {ex.Message}");
        }
        return result;
    }
}