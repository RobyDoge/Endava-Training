namespace HomeConsole.Models.Interfaces;

internal interface ITemperatureControl
{
    double TargetCelsius { get; }

    void SetTarget(double celsius);
}