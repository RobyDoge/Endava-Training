namespace HomeConsole.Models.Interfaces;

internal interface IMeasurableLoad
{
    double CurrentWatts { get; }
    double TotalWh { get; }

    void ResetEnergy();
}