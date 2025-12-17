using AirportTool.Application.Records;
using AirportTool.Domain.Errors;
using CSharpFunctionalExtensions;

namespace AirportTool.Application.Abstractions;

public interface IFlightRepository
{
    Task<Result<IIdentifiable<int>, Error>> CreateAsync(CreateFlightRecord record);

    Task<UnitResult<Error>> UpdateAsync(int id,
        UpdateFlightRecord record);

    Task<UnitResult<Error>> DeleteAsync(int id);
}