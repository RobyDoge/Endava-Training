using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkCore.Domain;

public abstract class BaseDomainModel
{
    public uint Id { get; set; }
    public DateTime CreatedDate { get; set; }
}