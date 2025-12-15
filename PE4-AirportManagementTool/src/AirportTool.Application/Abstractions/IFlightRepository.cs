using CSharpFunctionalExtensions;

namespace AirportTool.Application.Abstractions;

public interface IFlightRepository
{
    Task<Result<IIdentifiable<int>>> CreateAsync(string airlineIata,
        string flightNumber,
        string originIata,
        string destinationIata,
        string? defaultAircraftTailName);

    Task<Result> UpdateAsync(int id,
        string? airlineIata,
        string? flightNumber,
        string? originIata,
        string? destinationIata,
        string? defaultAircraftTailName,
        bool isActive);

    Task<Result> DeleteAsync(int id);
}