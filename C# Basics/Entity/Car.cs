using Basics.Base;

namespace Basics.Entity;

public class Car: Vehicle
{
    public int FlatFeeKm { get; set; } = 2;
    public int FlatFeeDistance { get; set; } = 2;

}
