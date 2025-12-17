using AirportTool.Application.Records;
using AirportTool.Domain.Entities;
using AirportTool.Domain.Errors;
using CSharpFunctionalExtensions;

namespace AirportTool.Application.Abstractions;

public interface IFlightScheduleRepository
{
    Task<Result<IEnumerable<FlightScheduleEntity>, Error>> GetByRouteAndDateAsync(GetFlightsByRouteAndDateRecord record);
}