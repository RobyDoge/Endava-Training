using CSharpFunctionalExtensions;

namespace AirportTool.Application.Abstractions;

public interface IFlightRepository
{
    Task<Result<IIdentifiable<int>>> CreateAsync(string airlineIata,
        string flightNumber,
        string originIata,
        string destinationIata,
        string? defaultAircraftTailName);
}