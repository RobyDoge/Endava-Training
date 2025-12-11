using AirportTool.Application.Abstractions;
using AirportTool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirportTool.Application.Services;

public class FlightScheduleService
{
    private IUnitOfWork UnitOfWork { get; }

    public FlightScheduleService(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<FlightScheduleEntity>> GetByRouteAndDate(string fromIata, string toIata, DateTime date)
    {
        var result = await UnitOfWork.FlightScheduleRepository.GetByRouteAndDateAsync(fromIata, toIata, date);
        await UnitOfWork.SaveChangesAsync();
        return result;
    }
}