using AirportTool.Application.Records;
using AirportTool.Application.Services;
using AirportTool.Domain.Entities;
using AirportTool.WebAPI.Models.DTOs;
using AirportTool.WebAPI.Models.Requests;
using AirportTool.WebAPI.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirportTool.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingController : ControllerBase
{
    public IMapper Mapper { get; }
    public BookingService BookingService { get; }

    public BookingController(IMapper mapper, BookingService bookingService)
    {
        Mapper = mapper;
        BookingService = bookingService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBooking(CreateBookingRequest request)
    {
        var record = Mapper.Map<CreateBookingRequest, CreateBookingRecord>(request);
        var result = await BookingService.CreateAsync(record);

        if (result.IsFailure) return Converter.ErrorToActionResult(result.Error);

        var response = Mapper.Map<BookingEntity, CreatBookingDto>(result.Value);
        return Ok(response);
    }

    [HttpGet("{code}")]
    public async Task<IActionResult> GetBookingByCode(string code)
    {
        if (string.IsNullOrEmpty(code) || code.Length > 8)
        {
            return BadRequest("Invalid confirmation code.");
        }

        var result = await BookingService.GetByConfirmationCodeAsync(code);
        if (result.IsFailure) return Converter.ErrorToActionResult(result.Error);

        var response = Mapper.Map<BookingEntity, BookingDto>(result.Value);
        return Ok(response);
    }

    [HttpDelete("{code}")]
    public async Task<IActionResult> CancelBooking(string code)
    {
        if (string.IsNullOrEmpty(code) || code.Length > 8)
        {
            return BadRequest("Invalid confirmation code.");
        }

        var result = await BookingService.CancelAsync(code);
        if (result.IsFailure) return Converter.ErrorToActionResult(result.Error);

        return NoContent();
    }
}