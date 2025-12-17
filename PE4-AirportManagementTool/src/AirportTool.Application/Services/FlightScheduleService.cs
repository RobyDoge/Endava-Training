using AirportTool.Application.Abstractions;
using AirportTool.Application.Records;
using AirportTool.Domain.Entities;
using AirportTool.Domain.Errors;
using CSharpFunctionalExtensions;

namespace AirportTool.Application.Services;

public class FlightScheduleService
{
    private IUnitOfWork UnitOfWork { get; }

    public FlightScheduleService(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<FlightScheduleEntity>, Error>> GetByRouteAndDate(GetFlightsByRouteAndDateRecord record)
    {
        var result = await UnitOfWork.FlightScheduleRepository.GetByRouteAndDateAsync(record);
        return result;
    }
}