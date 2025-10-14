using HomeConsole.Models.Interfaces;

namespace HomeConsole.Models;

internal class SmartPlug : SmartDevice, IMeasurableLoad
{
    public double CurrentWatts { get; set; }

    public double TotalWh { get; set; }

    public void ResetEnergy() => TotalWh = 0;
    public override string SelfTest() => $"SmartPlug {Name}";
}
