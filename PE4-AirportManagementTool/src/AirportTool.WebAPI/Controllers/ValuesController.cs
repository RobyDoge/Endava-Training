using AirportTool.Application.Services;
using AirportTool.Domain.Entities;
using AirportTool.Infrastructure.Context;
using AirportTool.Infrastructure.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AirportTool.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    private AirlineBookingContext db { get; set; }
    private IMapper Mapper { get; set; }
    private FlightScheduleService fss { get; set; }

    public ValuesController(AirlineBookingContext airport, IMapper mapper, FlightScheduleService fsss)
    {
        db = airport;
        Mapper = mapper;
        fss = fsss;
    }

    [HttpGet]
    public async Task<FlightScheduleEntity> GetAircraft()
    {
        var result = await fss.GetByRouteAndDate("otp", "LTn", DateTime.Parse("2025-05-01"));
        return result.FirstOrDefault();
    }
}