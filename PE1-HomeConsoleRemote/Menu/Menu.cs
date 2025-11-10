using HomeConsole.Models;
using HomeConsole.Services;

namespace HomeConsole.Menu;

internal class Menu
{
    private IDeviceRegistry DeviceRegistry { get; set; } = new DeviceRegistry();
    private int _currentId = 1;

    public Menu()
    {
        DeviceRegistry.Register(new ColorBulb(_currentId++, "Bedroom Light", brightness: 100, red: 255, blue: 0, green: 123, temperature: 100));
        DeviceRegistry.Register(new LightBulb(_currentId++, "Kitchen Light", brightness: 75));
        DeviceRegistry.Register(new SmartPlug(_currentId++, "Coffee Maker", currentWatts: 12));
        DeviceRegistry.Register(new Thermostat(_currentId++, "Main Hall", targetCelsius: 22));
        DeviceRegistry.Get<LightBulb>(2)?.PowerOn();
        DeviceRegistry.Get<SmartPlug>(3)?.PowerOn();
    }

    public void Run()
    {
        do
        {
            Console.WriteLine();
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
            switch (input)
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
    }

    private void SelfTestAll()
    {
        foreach (var device in DeviceRegistry.GetAll())
        {
            Console.WriteLine(device.SelfTest());
        }
    }

    private void DeviceActions()
    {
        Console.WriteLine("Insert the id of the device you want to interact with: ");
        try
        {
            int id = int.Parse(Console.ReadLine());
            var device = DeviceRegistry.TryGetDevice(id, out var foundDevice) ? foundDevice : null;
            if (device is null)
            {
                Console.WriteLine("Device not found");
                return;
            }
            switch (device)
            {
                case LightBulb bulb:
                    Console.WriteLine("Insert the brightness (between 0-100): ");
                    if (!int.TryParse(Console.ReadLine(), out int brightness))
                        throw new FormatException("Invalid input format. Please ensure temperature is a number.");
                    bulb.SetBrightness(brightness);
                    Console.WriteLine($"Lightbulb was adjusted");
                    break;

                case ColorBulb colorBulb:
                    Console.WriteLine("Insert the brightness (between 0-100), RGB values (in this order; between 0-255), and temperature (between 0-100):");
                    string[] colorParts = Console.ReadLine().Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (colorParts.Length < 5)
                    {
                        Console.WriteLine("Invalid input, please provide brightness, RGB values and temperature.");
                        return;
                    }
                    colorBulb.SetColor(byte.Parse(colorParts[1]), byte.Parse(colorParts[2]), byte.Parse(colorParts[3]));
                    colorBulb.SetBrightness(int.Parse(colorParts[0]));
                    colorBulb.SetTemperature(int.Parse(colorParts[4]));
                    Console.WriteLine("Colorbulb was adjusted");
                    break;

                case Thermostat thermostat:
                    Console.WriteLine("Set new target value (between 10 and 30):");
                    if (!int.TryParse(Console.ReadLine(), out int targetCelsius))
                        throw new FormatException("Invalid input format. Please ensure temperature is a number.");
                    thermostat.SetTarget(targetCelsius);
                    Console.WriteLine("Thermostat was adjusted");
                    break;

                case SmartPlug smartPlug:
                    Console.WriteLine("View current load(0) or reset the energy counter(1)");
                    if (!int.TryParse(Console.ReadLine(), out int choice) && (choice == 1 || choice == 0))
                        throw new FormatException("Invalid input format. Please ensure input is 0 or 1.");
                    if (choice == 0)
                    {
                        Console.WriteLine($"Current load is: {smartPlug.CurrentWatts}");
                        break;
                    }
                    smartPlug.ResetEnergy();
                    Console.WriteLine("Energy counter was reset");
                    break;
            }
        }
        catch (FormatException fe)
        {
            Console.WriteLine(fe.Message);
            return;
        }
        catch (ArgumentOutOfRangeException aoore)
        {
            Console.WriteLine(aoore.Message);
            return;
        }
        catch (OverflowException oe)
        {
            Console.WriteLine(oe.Message);
            return;
        }
    }

    private void TogglePower()
    {
        Console.WriteLine("Insert the id of device u want to change the power");
        try
        {
            int id = int.Parse(Console.ReadLine());
            var device = DeviceRegistry.TryGetDevice(id, out var foundDevice) ? foundDevice : null;
            if (device is null)
            {
                Console.WriteLine("Device not found");
                return;
            }
            Console.WriteLine("Device found: " + device.Name + ", current status: " + (device.GetStatus() ? "On" : "Off"));
            Console.WriteLine("Switching device to " + (device.GetStatus() ? "Off" : "On"));
            if (device.GetStatus())
            {
                device.PowerOff();
            }
            else
            {
                device.PowerOn();
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input format. Please ensure id is a number.");
            return;
        }
    }

    private void AddDevice()
    {
        Console.Write("To add a device include the following separated by blank spaces: type(LightBulb/ColorBulb/SmartPlug/Thermostat) name: ");
        string input = Console.ReadLine().Trim();
        string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length < 2)
        {
            Console.WriteLine("Invalid input, please provide at least type and name.");
            return;
        }
        switch (parts[0].ToUpper())
        {
            case "LIGHTBULB":
                AddLightBulb(parts[1]);
                return;

            case "COLORBULB":
                AddColorBulb(parts[1]);
                return;

            case "SMARTPLUG":
                AddSmartPlug(parts[1]);
                return;

            case "THERMOSTAT":
                AddThermostat(parts[1]);
                return;

            default:
                Console.WriteLine("Invalid Option please choice one of the above.");
                return;
        }
    }

    private void AddThermostat(string name)
    {
        Console.WriteLine("Insert the target temperature (between 10-30): ");
        try
        {
            int targetCelsius = int.Parse(Console.ReadLine());
            DeviceRegistry.Register(new Thermostat(_currentId++, name, targetCelsius));
            Console.WriteLine("Thermostat added succesfully");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input format. Please ensure temperature is a number.");
            return;
        }
        return;
    }

    private void AddSmartPlug(string name)
    {
        DeviceRegistry.Register(new SmartPlug(_currentId++, name));
        Console.WriteLine("SmartPlug added succesfully");
        return;
    }

    private void AddLightBulb(string name)
    {
        Console.WriteLine("Insert the brightness (between 0-100): ");
        try
        {
            int brightness = int.Parse(Console.ReadLine());
            DeviceRegistry.Register(new LightBulb(_currentId++, name, brightness));
            Console.WriteLine("LightBulb added succesfully");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input format. Please ensure brightness is a number.");
            return;
        }
        return;
    }

    private void AddColorBulb(string name)
    {
        Console.WriteLine("Insert the brightness (between 0-100), RGB values (in this order; between 0-255), and temperature (between 0-100):");
        string[] colorParts = Console.ReadLine().Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (colorParts.Length < 5)
        {
            Console.WriteLine("Invalid input, please provide brightness, RGB values and temperature.");
            return;
        }
        try
        {
            DeviceRegistry.Register(new ColorBulb(_currentId++, name, int.Parse(colorParts[0]), byte.Parse(colorParts[1]), byte.Parse(colorParts[2]), byte.Parse(colorParts[3]), int.Parse(colorParts[4])));
            Console.WriteLine("ColorBulb added succesfully");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input format. Please ensure all values are numbers.");
            return;
        }
        catch (OverflowException)
        {
            Console.WriteLine("Invalid input. RGB value must be between 0 and 255.");
        }
        return;
    }

    private void ListDevices()
    {
        Console.WriteLine("ID  | Name           | Type       | Status | Extra Information");
        Console.WriteLine("------------------------------------------------------------");
        foreach (var device in DeviceRegistry.GetAll())
        {
            string name = string.Format("{0, -15}", device.Name);
            string id = string.Format("{0,-2}", device.Id.ToString());
            string status = string.Format("{0,-6}", device.GetStatus() ? "On" : "Off");
            string type = string.Format("{0,-10}", device.GetType().Name);
            string extraInfo = device switch
            {
                LightBulb => $"Brightness: {(device as LightBulb)?.Brightness}%",
                ColorBulb => $"Brightness: {(device as ColorBulb)?.Brightness}%, Color: RGB({(device as ColorBulb)?.RedValue}, {(device as ColorBulb)?.GreenValue}, {(device as ColorBulb)?.BlueValue}), Temp: {(device as ColorBulb)?.Temperature}",
                Thermostat => $"Target: {(device as Thermostat)?.TargetCelsius}°C",
                SmartPlug => $"Current Watts: {(device as SmartPlug)?.CurrentWatts}W, Total Watts: {(device as SmartPlug)?.TotalWh}",
                _ => ""
            };
            Console.WriteLine($"{id} | {name} | {type} | {status} | {extraInfo}");
        }
    }
}