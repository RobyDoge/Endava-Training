using AirportTool.Application.Records;
using AirportTool.Application.Services;
using AirportTool.WebAPI.Models.DTOs;
using AirportTool.WebAPI.Models.Requests;
using AirportTool.WebAPI.Utils;
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
        var result = await TicketsService.GetByFlightAsync(flightId);
        if (result.IsFailure) return Converter.ErrorToActionResult(result.Error);

        var response = Mapper.Map<List<GetTicketDto>>(result.Value);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTicket(CreateTicketRequest createTicketRequest)
    {
        var record = Mapper.Map<CreateTicketRecord>(createTicketRequest);
        var result = await TicketsService.CreateAsync(record);
        if (result.IsFailure) return Converter.ErrorToActionResult(result.Error);
        return Ok(result.Value);
    }

    [HttpPut("{id}/inventory")]
    public async Task<IActionResult> UpdateTicketInventory(int id, int newSeatInvetoryCount)
    {
        var record = new UpdateTicketRecord { SeatInventory = newSeatInvetoryCount };
        var result = await TicketsService.UpdateAsync(id, record);
        if (result.IsFailure) return Converter.ErrorToActionResult(result.Error);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTicket(int id)
    {
        var result = await TicketsService.DeleteAsync(id);
        if (result.IsFailure) return Converter.ErrorToActionResult(result.Error);
        return Ok();
    }
}