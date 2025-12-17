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
        CreateMap<CreateTicketRequest, CreateTicketRecord>();
    }
}