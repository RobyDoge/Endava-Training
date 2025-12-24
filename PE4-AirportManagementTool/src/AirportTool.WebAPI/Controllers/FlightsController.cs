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
public class FlightsController : ControllerBase
{
    private FlightService FlightService { get; }
    private IMapper Mapper { get; }

    public FlightsController(FlightService flightService, IMapper mapper)
    {
        Mapper = mapper;
        FlightService = flightService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateFlight(CreateFlightRequest request)
    {
        var createFlightRecord = Mapper.Map<CreateFlightRecord>(request);
        var result = await FlightService.CreateAsync(createFlightRecord);
        if (result.IsFailure) return Converter.ErrorToActionResult(result.Error);

        var flightDto = Mapper.Map<FlightDto>(result.Value);
        return Ok(flightDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFlight(int id, UpdateFlightRequest request)
    {
        var record = Mapper.Map<UpdateFlightRecord>(request);
        var result = await FlightService.UpdateAsync(id, record);
        return result.IsSuccess ? NoContent() : Converter.ErrorToActionResult(result.Error);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFlight(int id)
    {
        var result = await FlightService.DeleteAsync(id);
        return result.IsSuccess ? NoContent() : Converter.ErrorToActionResult(result.Error);
    }
}