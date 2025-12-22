using AirportTool.Application.Records;
using AirportTool.Application.Services;
using AirportTool.WebAPI.Models.DTOs;
using AirportTool.WebAPI.Models.Requests;
using AirportTool.WebAPI.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirportTool.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FlightScheduleController : ControllerBase
{
    public IMapper Mapper { get; }
    public FlightScheduleService FlightScheduleService { get; }

    public FlightScheduleController(IMapper mapper, FlightScheduleService flightScheduleService)
    {
        Mapper = mapper;
        FlightScheduleService = flightScheduleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetByRouteAndDate([FromQuery] GetFlightsByRouteAndDateRequest request)
    {
        var record = Mapper.Map<GetFlightsByRouteAndDateRecord>(request);
        var result = await FlightScheduleService.GetByRouteAndDate(record);
        if (result.IsFailure) return Converter.ErrorToActionResult(result.Error);

        var response = Mapper.Map<List<GetFlightScheduleDto>>(result.Value);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await FlightScheduleService.GetById(id);
        if (result.IsFailure) return Converter.ErrorToActionResult(result.Error);

        var response = Mapper.Map<GetFlightScheduleDto>(result.Value);
        return Ok(response);
    }

    [HttpGet("/stats/upcoming")]
    public async Task<IActionResult> GetUpcoming(DateTime date)
    {
        var result = await FlightScheduleService.GetUpcoming(date, date.AddDays(7));
        if (result.IsFailure) return Converter.ErrorToActionResult(result.Error);

        var flightsPerDate = result.Value
            .GroupBy(f => f.ScheduledDepartureUtc.Date)
            .ToDictionary(g => g.Key, g => g.Count());

        var response = flightsPerDate
                .Select(kvp => new UpcomingFlightsDto
                {
                    date = kvp.Key,
                    flightCount = kvp.Value
                })
                .ToList();

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateFlightScheduleRequest request)
    {
        var record = Mapper.Map<CreateFlightScheduleRequest, CreateFlightScheduleRecord>(request);
        var result = await FlightScheduleService.CreateAsync(record);
        if (result.IsFailure) return Converter.ErrorToActionResult(result.Error);

        return Ok(result.Value);
    }
}