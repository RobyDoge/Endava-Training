using System;
using System.Collections.Generic;
using System.Text;

namespace AirportTool.Infrastructure.Models.Enums;

public enum FlightScheduleStatusEnum
{
    Planned = 1,
    Boarding = 2,
    Departed = 3,
    Canceled = 4,
    Delayed = 5
}