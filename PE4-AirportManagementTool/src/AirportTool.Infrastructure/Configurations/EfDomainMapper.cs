using AutoMapper;

namespace AirportTool.Infrastructure.Configurations;

public class EfDomainMapper : Profile
{
    public EfDomainMapper()
    {
        CreateMap<Models.Airline, Domain.Models.Airline>().ReverseMap().;
    }
}