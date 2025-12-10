using AirportTool.Domain.Models;
using AirportTool.Infrastructure.Context;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirportTool.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    AirlineBookingContext db { get; set; }
    IMapper Mapper { get; set; }
    public ValuesController(AirlineBookingContext airport, IMapper mapper)
    {
        db = airport;
        Mapper = mapper;
    }

    [HttpGet]
    public Airline GetAircraft()
    {
        var airline = db.Airlines.Find(1);
        return Mapper.Map<Airline>(airline);
    }
}
