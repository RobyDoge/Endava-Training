using AirportTool.Infrastructure.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AirportTool.Infrastructure.Models;

public partial class FlightSchedule
{
    [NotMapped]
    public FlightScheduleStatusEnum Status
    {
        get => (FlightScheduleStatusEnum)FlightScheduleId;
        set => FlightScheduleId = (int)value;
    }
}