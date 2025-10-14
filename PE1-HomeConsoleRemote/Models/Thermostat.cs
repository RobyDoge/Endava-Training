using HomeConsole.Models.Interfaces;

namespace HomeConsole.Models;

internal class Thermostat : SmartDevice, ITemperatureControl
{
    public double TargetCelsius { get; } = 20.0;

    public void SetTarget(double celsius)
    {
        if (celsius < 10.0 || celsius > 30.0)
        {
            throw new ArgumentOutOfRangeException(nameof(celsius), "Target temperature must be between 10.0 and 30.0 degrees Celsius.");

        }
    }
    public override string SelfTest() => $"Thermostat {Name}";
}
