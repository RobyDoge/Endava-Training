using AirportTool.Application.Records;
using AirportTool.WebAPI.Models.Requests;
using AutoMapper;

namespace AirportTool.WebAPI.Configurations;

public class RequestRecordProfile : Profile
{
    public RequestRecordProfile()
    {
        CreateMap<CreateFlightRequest, CreateFlightRecord>();
        CreateMap<UpdateFlightRequest, UpdateFlightRecord>();
        CreateMap<GetFlightsByRouteAndDateRequest, GetFlightsByRouteAndDateRecord>();
        CreateTicketRecordMap();
    }

    private void CreateTicketRecordMap()
    {
        CreateMap<CreateTicketRequest, CreateTicketRecord>()
            .ForMember(dest => dest.FareClass,
            opt => opt.MapFrom(src => Enum.Parse<Domain.Enums.FareClassEnum>(src.FareClass, true)))
            .ForMember(dest => dest.Currency,
            opt => opt.MapFrom(src => Enum.Parse<Domain.Enums.CurrencyEnum>(src.Currency, true)));
    }
}