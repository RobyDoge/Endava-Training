using AirportTool.Application.Records;
using AirportTool.Domain.Entities;
using AirportTool.Domain.Errors;
using CSharpFunctionalExtensions;

namespace AirportTool.Application.Abstractions;

public interface IFlightScheduleRepository
{
    Task<Result<IEnumerable<FlightScheduleEntity>, Error>> GetByRouteAndDateAsync(GetFlightsByRouteAndDateRecord record);

    Task<Result<FlightEntity, Error>> GetByIdAsync(int flightId);

    Task<Result<IEnumerable<FlightScheduleEntity>, Error>> GetUpcomingAsync(DateTime startingDate, DateTime endingDate);

    Task<Result<IIdentifiable<int>, Error>> CreateAsync(CreateFlightScheduleRecord record);

    Task<Result<BulkImportFlightScheduleSummary>> BulkImport(IEnumerable<CreateFlightScheduleRecord> records);
}