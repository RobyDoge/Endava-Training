using AirportTool.Application.Abstractions;
using AirportTool.Application.Records;
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

    public async Task<Result<int, Error>> CreateAsync(CreateFlightRecord createFlightRecord)
    {
        if (!Validators.FlightValidator.IsValidFlightNumber(createFlightRecord.FlightNumber)) return Result.Failure<int, Error>(Error.Validation("Incorrect Flight Number Format"));
        if (!Validators.FlightValidator.AreAirportsDifferent(
            createFlightRecord.OriginAirportIata,
            createFlightRecord.DestinationAirportIata)) return Result.Failure<int, Error>(Error.Validation("Airports must be different"));

        var result = await UnitOfWork.FlightRepository.CreateAsync(createFlightRecord);

        await UnitOfWork.SaveChangesAsync();

        if (result.IsFailure) return Result.Failure<int, Error>(result.Error);

        return Result.Success<int, Error>(result.Value.Id);
    }

    public async Task<UnitResult<Error>> UpdateAsync(int id,
            UpdateFlightRecord record)
    {
        if (record.FlightNumber != null && !Validators.FlightValidator.IsValidFlightNumber(record.FlightNumber)) return UnitResult.Failure(Error.Validation("Invalid Flight Number"));
        if (record.OriginAirportIata != null && record.DestinationAirportIata != null && !Validators.FlightValidator.AreAirportsDifferent(record.OriginAirportIata, record.DestinationAirportIata)) return Result.Failure<int, Error>(Error.Validation("Airports must be different"));

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