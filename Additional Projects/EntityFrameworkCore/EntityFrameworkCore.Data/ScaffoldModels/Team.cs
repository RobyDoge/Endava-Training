using System;
using System.Collections.Generic;

namespace EntityFrameworkCore.Data.ScaffoldModels;

public partial class Team
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public DateTime CreatedDate { get; set; }
}
