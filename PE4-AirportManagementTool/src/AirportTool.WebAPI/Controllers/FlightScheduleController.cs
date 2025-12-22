using AirportTool.Application.Services;
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
}