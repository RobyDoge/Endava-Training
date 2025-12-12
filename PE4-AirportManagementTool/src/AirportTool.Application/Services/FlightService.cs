using AirportTool.Application.Abstractions;
using AirportTool.Domain.Entities;
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

    public async Task<int> CreateAsync(string airlineIata,
        string flightNumber,
        string originIata,
        string destinationIata,
        string? defaultAircraftTailName)
    {
        var result = await UnitOfWork.FlightRepository.CreateAsync(
        airlineIata,
        flightNumber,
        originIata,
        destinationIata,
        defaultAircraftTailName);

        await UnitOfWork.SaveChangesAsync();

        return result.Id;
    }
}