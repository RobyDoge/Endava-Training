using HomeConsole.Models.Interfaces;

namespace HomeConsole.Models;

internal class SmartPlug : SmartDevice, IMeasurableLoad
{
    public double CurrentWatts { get; set; } = 0;

    public double TotalWh { get; set; } = 0;
    public SmartPlug(int id, string name) : base(id, name)
    {
    }
    public SmartPlug(int id, string name, double currentWatts) : base(id, name)
    {
        CurrentWatts = currentWatts;
    }

    public void ResetEnergy() => TotalWh = 0;
    public override string SelfTest() => $"SmartPlug {Name}";
}
