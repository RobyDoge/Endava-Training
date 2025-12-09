using AirportTool.Infrastructure.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AirportTool.Infrastructure.Models;

public partial class Ticket
{
    [NotMapped]
    public CurrencyEnum CurrencyEnum
    {
        get => (CurrencyEnum)CurrencyId;
        set => CurrencyId = (int)value;
    }

    [NotMapped]
    public FareClassEnum FareCLassEnum
    {
        get => (FareClassEnum)FareClassId;
        set => FareClassId = (int)value;
    }
}