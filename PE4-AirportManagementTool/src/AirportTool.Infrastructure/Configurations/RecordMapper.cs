using AirportTool.Application.Records;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirportTool.Infrastructure.Configurations;

public class RecordMapper : Profile
{
    public RecordMapper()
    {
        CreateMap<UpsertFlightScheduleRecord, CreateFlightScheduleRecord>();
        CreateMap<UpsertFlightScheduleRecord, UpdateFlightScheduleRecord>();
    }
}