using HomeConsole.Models.Interfaces;

namespace HomeConsole.Models;

internal abstract class SmartDevice : IPowerSwitch, ISelfTest

{
    public int Id { get; init; }
    public string? Name { get; set; }

    private bool IsOn { get; set; }

    public SmartDevice(int id, string name)
    {
        Id = id;
        Name = name;
        IsOn = false;
    }

    public void PowerOn() => IsOn = true;

    public void PowerOff() => IsOn = false;

    public bool GetStatus() => IsOn;

    public virtual string SelfTest() => "SmartDevice";
}