using AirportTool.Domain.Entities;
using AirportTool.Domain.Enums;
using AirportTool.Infrastructure.Models;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace AirportTool.Infrastructure.Configurations;

public class EfDomainMapper : Profile
{
    public EfDomainMapper()
    {
        CreateMap<Airline, AirlineEntity>().ReverseMap();
        CreateMap<Airport, AirportEntity>().ReverseMap();
        CreateMap<Aircraft, AircraftEntity>().ReverseMap();
        CreateMap<Flight, FlightEntity>().ReverseMap();
        CreateMap<Gate, GateEntity>().ReverseMap();

        CreateMap<FlightSchedule, FlightScheduleEntity>().ConstructUsing(MapFlightScheduleEntity);
        MapFlightSchedule();

        MapTicketEntity();
        MapTicket();
    }

    private void MapTicketEntity()
    {
        CreateMap<Ticket, TicketEntity>()
        .ForCtorParam("currency",
            opt => opt.MapFrom(src => src.CurrencyId))          
        .ForCtorParam("fareClass",
            opt => opt.MapFrom(src => src.FareClassId))
        .ForMember(dest => dest.FlightSchedule,
        opt => opt.MapFrom(src => src.FlightSchedule));
    }

    private void MapTicket()
    {
        CreateMap<TicketEntity, Ticket>()
            .ForMember(dest => dest.FlightScheduleId,
            opt => opt.MapFrom(src => src.FlightSchedule.Id))
            .ForMember(dest => dest.CurrencyId,
            opt => opt.MapFrom(src => (int)src.Currency))
            .ForMember(dest => dest.FareClassId,
            opt => opt.MapFrom(src => (int)src.FareClass))
            .ForMember(dest => dest.FlightSchedule,
                opt => opt.Ignore())
            .ForMember(dest => dest.Currency,
                opt => opt.Ignore())
            .ForMember(dest => dest.FareClass,
                opt => opt.Ignore());
    }

    private static FlightScheduleEntity MapFlightScheduleEntity(FlightSchedule src, ResolutionContext ctx) =>
        new(
            src.Id,
            src.ScheduledDepartureUtc,
            src.ScheduledArrivalUtc,
            ctx.Mapper.Map<FlightEntity>(src.Flight),
            (FlightScheduleStatusEnum)src.FlightScheduleStatusId,
            ctx.Mapper.Map<GateEntity>(src.Gate),
            src.AssignedAircraftId.HasValue
                ? ctx.Mapper.Map<AircraftEntity>(src.AssignedAircraft!)
                : null

        );

    private void MapFlightSchedule()
    {
        CreateMap<FlightScheduleEntity, FlightSchedule>()
            .ForMember(dest => dest.FlightId,
            opt => opt.MapFrom(src => src.Flight.Id))
            .ForMember(dest => dest.GateId,
            opt => opt.MapFrom(src => src.Gate.Id))
            .ForMember(dest => dest.FlightScheduleStatusId,
            opt => opt.MapFrom(src => (int)src.Status))
            .ForMember(dest => dest.AssignedAircraftId,
            opt => opt.MapFrom(src => src.AssignedAircraft != null ? src.AssignedAircraft.Id : (int?)null))
            .ForMember(dest => dest.Flight,
                opt => opt.Ignore())
            .ForMember(dest => dest.Gate,
                opt => opt.Ignore())
            .ForMember(dest => dest.Tickets,
                opt => opt.Ignore())
            .ForMember(dest => dest.AssignedAircraft,
                opt => opt.Ignore());
    }
}