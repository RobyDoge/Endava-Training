﻿
using HomeConsole.Models.Interfaces;

namespace HomeConsole.Models;

internal class LightBulb : SmartDevice, IDimmable
{

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

