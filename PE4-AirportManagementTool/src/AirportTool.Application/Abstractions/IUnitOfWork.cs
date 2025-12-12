using System;
using System.Collections.Generic;
using System.Text;

namespace AirportTool.Application.Abstractions;

public interface IUnitOfWork : IDisposable
{
    IFlightScheduleRepository FlightScheduleRepository { get; }
    IFlightRepository FlightRepository { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}