using HomeConsole.Models.Interfaces;

namespace HomeConsole.Models;

internal class Thermostat : SmartDevice, ITemperatureControl
{
    public double TargetCelsius { get; private set; } = 20.0;

    public Thermostat(int id, string name) : base(id, name)
    {
    }
    public Thermostat(int id, string name, double targetCelsius) : base(id, name)
    {
        SetTarget(targetCelsius);
    }
    public void SetTarget(double celsius)
    {
        if (celsius < 10.0 || celsius > 30.0)
        {
            throw new ArgumentOutOfRangeException(nameof(celsius), "Target temperature must be between 10.0 and 30.0 degrees Celsius.");

        }
        TargetCelsius = celsius;
    }
    public override string SelfTest() => $"Thermostat {Name}";
}
