namespace HomeConsole.Models.Interfaces;

internal interface IColorControl
{
    byte RedValue { get;}
    byte GreenValue { get;}
    byte BlueValue { get; } 
    int Temperature { get;}
    void SetColor(byte red, byte green, byte blue);
    void SetTemperature(int temperature);
}
