using HomeConsole.Models;
using System.Collections.Concurrent;

namespace HomeConsole.Services;

internal class DeviceRegistry : IDeviceRegistry
{
    private ConcurrentDictionary<int, SmartDevice> Devices { get; } = new();

    public event Action<SmartDevice>? DeviceRegistered;

    public event Action<int>? DeviceUnregistered;

    public IEnumerable<TCapability> FindByCapability<TCapability>() where TCapability : class
    {
        return Devices.Values.Select(device => device as TCapability).Where(capability => capability is not null)!;
    }

    public T? Get<T>(int deviceId) where T : SmartDevice
    {
        return Devices.TryGetValue(deviceId, out var device) ? device as T : null;
    }

    public IEnumerable<SmartDevice> GetAll()
    {
        return Devices.Values;
    }

    public IEnumerable<T> GetAll<T>() where T : SmartDevice
    {
        return Devices.Values.OfType<T>();
    }

    public bool Register<T>(T device) where T : SmartDevice
    {
        if (device is null) return false;
        var added = Devices.TryAdd(device.Id, device);
        if (added) DeviceRegistered?.Invoke(device);
        return added;
    }

    public bool TryGetDevice(int deviceId, out SmartDevice? device)
    {
        var found = Devices.TryGetValue(deviceId, out device);
        return found;
    }

    public bool Unregister(int deviceId)
    {
        var found = Devices.TryRemove(deviceId, out var device);
        if (found) DeviceUnregistered?.Invoke(deviceId);
        return found;
    }
}