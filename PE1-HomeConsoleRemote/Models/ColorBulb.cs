using HomeConsole.Models.Interfaces;
using System.Data;

namespace HomeConsole.Models;

internal class ColorBulb : SmartDevice, IDimmable, IColorControl
{
    public int Brightness { get; private set; } = 0;
    public byte RedValue { get; private set; }
    public byte GreenValue { get; private set; }
    public byte BlueValue { get; private set; }
    public int Temperature { get; private set; }

    public void SetBrightness(int brightness)
    {
        if (brightness < 0 || brightness > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(brightness), "Brightness must be between 0 and 100.");
        }
        Brightness = brightness;
    }

    public void SetColor(byte red, byte green, byte blue)
    {
        if (red>= 0 && blue >= 0 && green >= 0)
        {
            RedValue = red;
            GreenValue = green;
            BlueValue = blue;
            return;
        }
        throw new ArgumentOutOfRangeException("Color values must be between 0 and 255.");
    }

    public void SetTemperature(int temperature)
    {
        if (temperature < 0 || temperature > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(temperature), "Color temperature must be between 0 and 100.");)
        }
    }
}
