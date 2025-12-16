using AirportTool.Application.Abstractions;
using AirportTool.Domain.Entities;
using AirportTool.Domain.Errors;
using CSharpFunctionalExtensions;
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

    public FlightService(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    public async Task<Result<int, Error>> CreateAsync(string airlineIata,
        string flightNumber,
        string originIata,
        string destinationIata,
        string? defaultAircraftTailName)
    {
        if (!Validators.FlightValidator.IsValidFlightNumber(flightNumber)) return Result.Failure<int, Error>(Error.Validation("Incorrect Flight Number Format"));
        if (!Validators.FlightValidator.AreAirportsDifferent(originIata, destinationIata)) return Result.Failure<int, Error>(Error.Validation("Airports must be different"));

        var result = await UnitOfWork.FlightRepository.CreateAsync(
        airlineIata,
        flightNumber,
        originIata,
        destinationIata,
        defaultAircraftTailName);

        await UnitOfWork.SaveChangesAsync();

        if (result.IsFailure) return Result.Failure<int, Error>(result.Error);

        return Result.Success<int, Error>(result.Value.Id);
    }

    public async Task<UnitResult<Error>> UpdateAsync(int id,
        string? airlineIata,
        string? flightNumber,
        string? originIata,
        string? destinationIata,
        string? defaultAircraftTailName,
        bool? isActive)
    {
        if (flightNumber != null && !Validators.FlightValidator.IsValidFlightNumber(flightNumber)) return UnitResult.Failure(Error.Validation("Invalid Flight Number"));
        if (originIata != null && destinationIata != null && !Validators.FlightValidator.AreAirportsDifferent(originIata, destinationIata)) return Result.Failure<int, Error>(Error.Validation("Airports must be different"));

        var result = await UnitOfWork.FlightRepository.UpdateAsync(
        id,
        airlineIata,
        flightNumber,
        originIata,
        destinationIata,
        defaultAircraftTailName,
        isActive);

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