using AirportTool.Application.Abstractions;
using AirportTool.Infrastructure.Context;
using AirportTool.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirportTool.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private AirlineBookingContext Context { get; }
    public IFlightScheduleRepository FlightScheduleRepository { get; }
    public IFlightRepository FlightRepository { get; }

    public UnitOfWork(AirlineBookingContext context,
        IFlightScheduleRepository flightScheduleRepository,
        IFlightRepository flightRepository)
    {
        Context = context;
        FlightScheduleRepository = flightScheduleRepository;
        FlightRepository = flightRepository;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => Context.SaveChangesAsync(cancellationToken);

    public void Dispose()
    {
        Context.Dispose();
    }
}