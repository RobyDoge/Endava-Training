using DroneFleet.Enums;
using DroneFleet.Models;
using DroneFleet.Models.Interfaces;
using DroneFleet.Services;
using System.Data;
using System.Runtime.InteropServices;

List<Drone> drones = new();
int currentId = 0;
drones.Add(DroneFactory.CreateDrone(new DroneSpecs { Type = DroneType.Racing, BatteryPercent = 100, Name = "FastBoi", Id = ++currentId }));
drones.Add(DroneFactory.CreateDrone(new DroneSpecs { Type = DroneType.Delivery, BatteryPercent = 56, Name = "CarrialBoi", Id = ++currentId, CapacityKg = 20 }));
drones.Add(DroneFactory.CreateDrone(new DroneSpecs { Type = DroneType.Survey, BatteryPercent = 23, Name = "Paparazzi", Id = ++currentId }));
drones.Add(DroneFactory.CreateDrone(new DroneSpecs { Type = DroneType.Delivery, BatteryPercent = 19, Name = "OldMan", Id = ++currentId , CapacityKg = 1}));

drones.Find(x => x.Id == 2).TakeOff();
((DeliveryDrone)drones.Find(x => x.Id == 2)).SetWaypoint(23, 78);

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
    Console.WriteLine();

} while (true);

void ListDrones()
{
    foreach(var drone in drones)
    {
        string specialAttribute = string.Format("{0,-18}",drone switch
        {
            DeliveryDrone dd => $"Total capacity: {dd.CapacityKg.ToString() }", 
            SurveyDrone sd =>   $"Photos taken: {sd.PhotoCount.ToString()}",
            _ => ""
        });
        string waypoint = drone is INavigable ? $"Current waypoint { ((INavigable)drone).CurrentWaypoint.ToString()}" : "";

        string id = string.Format("{0, -3}", drone.Id.ToString());
        string name = string.Format("{0, -10}", drone.Name);
        string batteryPercentage = string.Format("{0,-3}", drone.BatteryPercent);
        string type = string.Format("{0,-13}", drone.GetType().Name.ToString());
        Console.WriteLine($"{id} | {name} | {type} | {batteryPercentage} | {specialAttribute} | {waypoint}");

    }
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
    Console.Write("Enter the id of the drone: ");
    bool isInt = int.TryParse(Console.ReadLine(), out int id);
    if(!isInt)
    {
        Console.WriteLine("Id must be an integer.");
        return;
    }
    var drone = drones.Find(x => x.Id == id);
    if (drone == null)
    {
        Console.WriteLine("Drone not found");
        return;
    }
    drone.BatteryPercent = 100;
    Console.WriteLine($"The battery for drone {drone.Name} was charged to {drone.BatteryPercent}.");

}