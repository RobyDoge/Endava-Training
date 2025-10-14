using HomeConsole.Models;
using HomeConsole.Services;

IDeviceRegistry deviceRegistry = new DeviceRegistry();
int currentId = 1;

deviceRegistry.Register(new ColorBulb(currentId++, "Bedroom Light", brightness: 100, red: 255, blue: 0, green: 123, temperature: 100));
deviceRegistry.Register(new LightBulb(currentId++, "Kitchen Light", brightness: 75));
deviceRegistry.Register(new SmartPlug(currentId++, "Coffee Maker",currentWatts:12));
deviceRegistry.Register(new Thermostat(currentId++, "Main Hall",targetCelsius:22));
deviceRegistry.Get<LightBulb>(2)?.PowerOn();
deviceRegistry.Get<SmartPlug>(3)?.PowerOn();

do
{
    Console.WriteLine("""
        === SMART HOME CONSOLE REMOTE ===
        1. List devices
        2. Add device
        3. Toggle power
        4. Device actions
        5. Self-test all
        6. Exit
        """);
    Console.Write("Select: ");
    int.TryParse(Console.ReadLine(), out int input);
    switch(input)
    {
        case 1: 
            ListDevices();
            break;
        case 2:
            AddDevice();
            break;
        case 3: 
            TogglePower();
            break;
        case 4:
            DeviceActions();
            break;
        case 5:
            SelfTestAll();
            break;
        case 6:
            return;
        default:
            Console.WriteLine("Invalid input, please chose again.");
            break;
    }

} while (true);

void SelfTestAll()
{
    throw new NotImplementedException();
}

void DeviceActions()
{
    throw new NotImplementedException();
}

void TogglePower()
{
    throw new NotImplementedException();
}

void AddDevice()
{
    throw new NotImplementedException();
}

void ListDevices()
{
    Console.WriteLine("ID  | Name           | Type       | Status | Extra Information");
    Console.WriteLine("------------------------------------------------------------");
    foreach (var device in deviceRegistry.GetAll())
    {
        string name = string.Format("{0, -15}",device.Name);
        string id = string.Format("{0,-2}",device.Id.ToString());
        string status = string.Format("{0,-6}",device.GetStatus() ? "On" : "Off");
        string type = string.Format("{0,-10}", device.GetType().Name);
        string extraInfo = device switch
        {
            LightBulb=> $"Brightness: {(device as LightBulb)?.Brightness}%",
            ColorBulb => $"Brightness: {(device as ColorBulb)?.Brightness}%, Color: RGB({(device as ColorBulb)?.RedValue}, {(device as ColorBulb)?.GreenValue}, {(device as ColorBulb)?.BlueValue}), Temp: {(device as ColorBulb)?.Temperature}",
            Thermostat => $"Target: {(device as Thermostat)?.TargetCelsius}°C",
            SmartPlug => $"Current Watts: {(device as SmartPlug)?.CurrentWatts}W, Total Watts: {(device as SmartPlug)?.TotalWh}",
            _ => ""
        };
        Console.WriteLine($"{id} | {name} | {type} | {status} | {extraInfo}");
        Console.WriteLine();


    }
}