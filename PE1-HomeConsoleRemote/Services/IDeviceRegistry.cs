using HomeConsole.Models;

namespace HomeConsole.Services;

internal interface IDeviceRegistry
{
    event Action<SmartDevice> DeviceRegistered;

    event Action<int> DeviceUnregistered;

    bool Register<T>(T device) where T : SmartDevice;

    bool Unregister(int deviceId);

    bool TryGetDevice(int deviceId, out SmartDevice? device);

    T? Get<T>(int deviceId) where T : SmartDevice;

    IEnumerable<SmartDevice> GetAll();

    IEnumerable<T> GetAll<T>() where T : SmartDevice;

    IEnumerable<TCapability> FindByCapability<TCapability>() where TCapability : class;
}