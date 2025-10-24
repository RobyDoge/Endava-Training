using DroneFleet.Models.Interfaces;
using System.Reflection.Metadata;
using System.Threading.Channels;

namespace DroneFleet.Models;

internal abstract class Drone : IFlightControl, ISelfTest
{
    public int Id { get; init; }
    public string Name { get; set; }
    public decimal BatteryPercent { get; set; }
    public bool IsAirborne { get; private set; }
    
    public Drone(int id, string name, decimal batteryPercent)
    {
        Id = id;
        Name = name;
        BatteryPercent = batteryPercent;
        IsAirborne = false;
    }

    public void Land()
    {
        IsAirborne = false;
    }

    public bool RunSelfTest()
    {
        if (BatteryPercent < 20) return false;
        return true;
    }

    public void TakeOff()
    {
        IsAirborne = true;

    }
}
