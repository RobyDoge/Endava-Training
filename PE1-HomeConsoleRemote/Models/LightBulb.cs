using HomeConsole.Models.Interfaces;

namespace HomeConsole.Models;

internal class LightBulb : SmartDevice, IDimmable
{
    public LightBulb(int id, string name) : base(id, name)
    {
    }

    public LightBulb(int id, string name, int brightness) : base(id, name)
    {
        SetBrightness(brightness);
    }

    public int Brightness { get; private set; } = 0;

    public void SetBrightness(int brightness)
    {
        if (brightness < 0 || brightness > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(brightness), "Brightness must be between 0 and 100.");
        }
        Brightness = brightness;
    }

    public override string SelfTest() => $"LightBulb {Name}";
}