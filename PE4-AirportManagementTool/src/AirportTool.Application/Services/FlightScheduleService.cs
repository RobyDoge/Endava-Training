using AirportTool.Application.Abstractions;
using AirportTool.Domain.Entities;
using CSharpFunctionalExtensions;

namespace AirportTool.Application.Services;

public class FlightScheduleService
{
    private IUnitOfWork UnitOfWork { get; }

    public FlightScheduleService(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<FlightScheduleEntity>>> GetByRouteAndDate(string fromIata, string toIata, DateTime date)
    {
        var result = await UnitOfWork.FlightScheduleRepository.GetByRouteAndDateAsync(fromIata, toIata, date);
        return result;
    }
}