using AirportTool.Application.Services;
using AirportTool.WebAPI.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirportTool.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FlightsController : ControllerBase
{
    private FlightScheduleService FlightScheduleService { get; }
    private IMapper Mapper { get; }

    public FlightsController(FlightScheduleService flightScheduleService, IMapper mapper)
    {
        FlightScheduleService = flightScheduleService;
        Mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetFlightsByRouteAndDate(GetFlightsByRouteAndDateRequest request)
    {
        var flightSchedules = await FlightScheduleService.GetByRouteAndDate(request.FromIata, request.ToIata, request.Date);
        var response = Mapper.Map<List<GetFlightScheduleDto>>(flightSchedules);
        return Ok(response);
    }
}