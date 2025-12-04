using System;
using System.Collections.Generic;

namespace EntityFrameworkCore.Data.ScaffoldModels;

public partial class Coach
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedDate { get; set; }
}
