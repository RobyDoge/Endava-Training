using Basics.Enum;
using Basics.Base;
namespace Basics.Entiry;

public class Driver :Person
{
    public Vehicle? Vehicle { get; set; }
    public decimal Rating { get; set; }
    public DriverStatus Status { get; set; } = DriverStatus.Unknown;

    public Driver(string name, Vehicle? vehicle, decimal rating)
    {
        Name = name;
        Vehicle = vehicle;
        Rating = rating;
    }

}
