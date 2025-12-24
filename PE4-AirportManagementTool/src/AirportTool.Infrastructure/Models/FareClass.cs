using System;
using System.Collections.Generic;

namespace AirportTool.Infrastructure.Models;

public partial class FareClass
{
    public string Code { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Id { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
