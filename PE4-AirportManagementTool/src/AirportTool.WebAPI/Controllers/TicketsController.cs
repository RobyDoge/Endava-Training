using AirportTool.Application.Services;
using AirportTool.WebAPI.Models.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirportTool.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketsController : ControllerBase
{
    private TicketsService TicketsService { get; }
    private IMapper Mapper { get; }

    public TicketsController(TicketsService ticketsService, IMapper mapper)
    {
        TicketsService = ticketsService;
        Mapper = mapper;
    }

    [HttpGet("by-flight/{flightId}")]
    public async Task<IActionResult> GetTicketsByFlight(int flightId)
    {
        var result = await TicketsService.GetTicketsByFlight(flightId);
        if (result.IsFailure) return BadRequest(result.Error);

        var response = Mapper.Map<List<GetTicketDto>>(result.Value);
        return Ok(response);
    }
}