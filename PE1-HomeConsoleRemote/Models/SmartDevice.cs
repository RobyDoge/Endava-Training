
using HomeConsole.Models.Interfaces;

namespace HomeConsole.Models;

internal abstract class SmartDevice: IPowerSwitch, ISelfTest

{
    public int Id { get; init; }
    public string? Name { get; set; }

    private bool IsOn { get; set; }

    public void PowerOn() => IsOn = true;
    public void PowerOff() => IsOn = false;
    public bool GetStatus() => IsOn;
    public virtual string SelfTest() => "SmartDevice";

    public void SetSchedule(TimeOnly start, TimeOnly end)
    {
        throw new NotImplementedException();
    }
}
