namespace HomeConsole.Models.Interfaces;

internal interface IColorControl
{
    byte RedValue { get; init; }
    byte GreenValue { get; init; }
    byte BlueValue { get; init; } 
    int Temperature { get; init; }
    void SetColor(byte red, byte green, byte blue);
    void SetTemperature(int temperature);
}
