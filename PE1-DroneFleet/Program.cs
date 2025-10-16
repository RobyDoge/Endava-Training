using DroneFleet.Enums;
using DroneFleet.Models;
using DroneFleet.Services;
using System.Runtime.InteropServices;

List<Drone> drones = new();
int currentId = 0;
drones.Add(DroneFactory.CreateDrone(new DroneSpecs { Type = DroneType.Racing, BatteryPercent = 56, Name = "FastBoi", Id = ++currentId }));
drones.Add(DroneFactory.CreateDrone(new DroneSpecs { Type = DroneType.Delivery, BatteryPercent = 56, Name = "CarrialBoi", Id = ++currentId, CapacityKg = 20 }));
drones.Add(DroneFactory.CreateDrone(new DroneSpecs { Type = DroneType.Survey, BatteryPercent = 56, Name = "Paparazzi", Id = ++currentId }));
drones.Add(DroneFactory.CreateDrone(new DroneSpecs { Type = DroneType.Delivery, BatteryPercent = 19, Name = "OldMan", Id = ++currentId , CapacityKg = 1}));

do
{
    Console.WriteLine("""
        === DRONE FLEET ===
        1. List drones
        2. Add drone
        3. Pre-flight check (all)
        4. Take off / Land
        5. Set waypoint
        6. Capability actions
        7. Charge battery
        8. Exit
        """);
    Console.Write("Select: ");
    bool isInt= int.TryParse(Console.ReadLine(), out int option);
    if (!isInt || (option < 1 || option > 8))
    {
        Console.WriteLine("Please selecte a valid option");
        continue; 
    }
    switch (option)
    {
        case 1:
            ListDrones();
            break;
        case 2:
            AddDrone();
            break;
        case 3:
            CheckAllDrones();
            break;
        case 4:
            TakeOffLandDrone();
            break;
        case 5:
            SetWaypoint();
            break;
        case 6:
            SpecialAction();
            break;
        case 7:
            ChargeBattery();
            break;
        case 8:
            return;
        default:
            Console.WriteLine("Invalid option. Please choice again!");
            break;
    }

} while (true);

void ListDrones()
{
    throw new NotImplementedException();
}

void AddDrone()
{
    throw new NotImplementedException();
}

void CheckAllDrones()
{
    throw new NotImplementedException();
}

void TakeOffLandDrone()
{
    throw new NotImplementedException();
}

void SetWaypoint()
{
    throw new NotImplementedException();
}

void SpecialAction()
{
    throw new NotImplementedException();
}

void ChargeBattery()
{
    throw new NotImplementedException();
}