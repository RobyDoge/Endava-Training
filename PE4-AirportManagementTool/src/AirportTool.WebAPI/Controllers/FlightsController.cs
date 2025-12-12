using AirportTool.Application.Services;
using AirportTool.WebAPI.Models.DTOs;
using AirportTool.WebAPI.Models.Requests;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirportTool.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FlightsController : ControllerBase
{
    private FlightScheduleService FlightScheduleService { get; }
    private FlightService FlightService { get; }
    private IMapper Mapper { get; }

    public FlightsController(FlightScheduleService flightScheduleService, FlightService flightService, IMapper mapper)
    {
        FlightScheduleService = flightScheduleService;
        Mapper = mapper;
        FlightService = flightService;
    }

    [HttpGet]
    public async Task<IActionResult> GetFlightsByRouteAndDate(GetFlightsByRouteAndDateRequest request)
    {
        var flightSchedules = await FlightScheduleService.GetByRouteAndDate(request.FromIata, request.ToIata, request.Date);
        if (flightSchedules == null || !flightSchedules.Any()) return NotFound();

        var response = Mapper.Map<List<GetFlightScheduleDto>>(flightSchedules);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateFlight(CreateFlightRequest request)
    {
        var result = await FlightService.CreateAsync(request.AirlineIata, request.FlightNumber, request.OriginAirportIata, request.DestinationAirportIata, request.DefaultAircraftTailName);
        if (result.IsFailure) return BadRequest(result.Error);
        return Ok(result.Value);
    }
}