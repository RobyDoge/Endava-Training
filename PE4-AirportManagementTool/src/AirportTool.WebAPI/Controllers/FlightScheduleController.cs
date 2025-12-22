using AirportTool.Application.Models;
using AirportTool.Application.Records;
using AirportTool.Application.Services;
using AirportTool.WebAPI.Models.DTOs;
using AirportTool.WebAPI.Models.Requests;
using AirportTool.WebAPI.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AirportTool.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FlightScheduleController : ControllerBase
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web)
    {
        PropertyNameCaseInsensitive = true
    };

    public IMapper Mapper { get; }
    public FlightScheduleService FlightScheduleService { get; }
    public AppSettings AppSettings { get; }

    public FlightScheduleController(IMapper mapper, FlightScheduleService flightScheduleService, AppSettings appSettings)
    {
        Mapper = mapper;
        FlightScheduleService = flightScheduleService;
        AppSettings = appSettings;
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

    [HttpPost("/import")]
    public async Task<IActionResult> BulkImport(IFormFile file)
    {
        if (file is null || file.Length == 0)
            return BadRequest("File must be not empty");

        if (!file.FileName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
            return BadRequest("File must be a json");

        List<UpsertFlightScheduleRecord>? rows;
        try
        {
            await using var stream = file.OpenReadStream();
            rows = await JsonSerializer.DeserializeAsync<List<UpsertFlightScheduleRecord>>(stream, JsonOptions);
        }
        catch (JsonException je)
        {
            return BadRequest($"Invalid JSON file. {je.Message}");
        }

        if (rows is null || rows.Count == 0)
            return BadRequest("JSON must be a non-empty array of schedule rows.");

        if (rows.Count > AppSettings.ImportLimit)
            return BadRequest($"There were {rows.Count} rows to import, but the limit it {AppSettings.ImportLimit}");

        var records = rows.ToAsyncEnumerable();
        var result = await FlightScheduleService.BulkImportAsync(records);

        return Ok(result);
    }
}