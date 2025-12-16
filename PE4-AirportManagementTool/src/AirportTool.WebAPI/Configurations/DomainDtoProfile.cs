using AirportTool.Domain.Entities;
using AirportTool.WebAPI.Models.DTOs;
using AutoMapper;

namespace AirportTool.WebAPI.Configurations;

public class DomainDtoProfile : Profile
{
    public DomainDtoProfile()
    {
        GetFlightScheduleMap();
        GetTicketMap();
    }

    private void GetTicketMap()
    {
        CreateMap<TicketEntity, GetTicketDto>()
            .ForMember(dest => dest.FareClass,
                opt => opt.MapFrom(src => src.FareClass.ToString()));
    }

    private void GetFlightScheduleMap()
    {
        CreateMap<FlightScheduleEntity, GetFlightScheduleDto>()
            .ForMember(dest => dest.FlightNumber,
                opt => opt.MapFrom(src => src.Flight.FlightNumber))
            .ForMember(dest => dest.DepartureTime,
                opt => opt.MapFrom(src => src.ScheduledDepartureUtc))
            .ForMember(dest => dest.ArrivalTime,
                opt => opt.MapFrom(src => src.ScheduledArrivalUtc))
            .ForMember(dest => dest.GateName,
                opt => opt.MapFrom(src => $"{src.Gate.Code}"))
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.AssignedAircraftTailName,
                opt => opt.MapFrom(src =>
                    src.AssignedAircraft != null
                        ? src.AssignedAircraft.TailName
                        : src.Flight.DefaultAircraft != null
                            ? src.Flight.DefaultAircraft.TailName
                            : "No Aircraft"
                ));
    }
}