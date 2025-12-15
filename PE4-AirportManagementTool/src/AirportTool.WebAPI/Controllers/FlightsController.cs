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
        var result = await FlightScheduleService.GetByRouteAndDate(request.FromIata, request.ToIata, request.Date);
        if (result.IsFailure) return BadRequest(result.Error);

        var response = Mapper.Map<List<GetFlightScheduleDto>>(result.Value);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateFlight(CreateFlightRequest request)
    {
        var result = await FlightService.CreateAsync(request.AirlineIata, request.FlightNumber, request.OriginAirportIata, request.DestinationAirportIata, request.DefaultAircraftTailName);
        if (result.IsFailure) return BadRequest(result.Error);
        return Ok(result.Value);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFlight(int id, UpdateFlightRequest request)
    {
        var result = await FlightService.UpdateAsync(id, request.AirlineIata, request.FlightNumber, request.OriginAirportIata, request.DestinationAirportIata, request.DefaultAircraftTailName, request.IsActive);
        if (result.IsFailure) return BadRequest(result.Error);
        return Ok();
    }
}