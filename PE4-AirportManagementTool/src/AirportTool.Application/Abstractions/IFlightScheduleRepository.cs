using AirportTool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirportTool.Application.Abstractions;

public interface IFlightScheduleRepository: IGenericRepository<FlightScheduleEntity>
{
}
