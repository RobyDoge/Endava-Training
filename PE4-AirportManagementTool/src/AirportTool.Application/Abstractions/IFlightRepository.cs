using AirportTool.Domain.Errors;
using CSharpFunctionalExtensions;

namespace AirportTool.Application.Abstractions;

public interface IFlightRepository
{
    Task<Result<IIdentifiable<int>, Error>> CreateAsync(string airlineIata,
        string flightNumber,
        string originIata,
        string destinationIata,
        string? defaultAircraftTailName);

    Task<UnitResult<Error>> UpdateAsync(int id,
        string? airlineIata,
        string? flightNumber,
        string? originIata,
        string? destinationIata,
        string? defaultAircraftTailName,
        bool? isActive);

    Task<UnitResult<Error>> DeleteAsync(int id);
}