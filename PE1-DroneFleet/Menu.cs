using DroneFleet.Enums;
using DroneFleet.Models;
using DroneFleet.Models.Interfaces;
using DroneFleet.Services;
using System;

namespace DroneFleet.Menu;
public class Menu
{
    private new List<Drone> Drones { get; set; } = [];
    private int currentId = -1;
    public Menu()
    {
        Drones.Add(DroneFactory.CreateDrone(new DroneSpecs { Type = DroneType.Racing, BatteryPercent = 100, Name = "FastBoi", Id = ++currentId }));
        Drones.Add(DroneFactory.CreateDrone(new DroneSpecs { Type = DroneType.Delivery, BatteryPercent = 56, Name = "CarrialBoi", Id = ++currentId, CapacityKg = 20 }));
        Drones.Add(DroneFactory.CreateDrone(new DroneSpecs { Type = DroneType.Survey, BatteryPercent = 23, Name = "Paparazzi", Id = ++currentId }));
        Drones.Add(DroneFactory.CreateDrone(new DroneSpecs { Type = DroneType.Delivery, BatteryPercent = 19, Name = "OldMan", Id = ++currentId, CapacityKg = 1 }));

        Drones.Find(x => x.Id == 2).TakeOff();
        ((DeliveryDrone)Drones.Find(x => x.Id == 2)).SetWaypoint(23, 78);

    }
    public void Run()
    {
        do
        {
            Console.WriteLine("""
        === DRONE FLEET ===
        1. List Drones
        2. Add drone
        3. Pre-flight check (all)
        4. Take off / Land
        5. Set waypoint
        6. Charge battery
        7. Exit
        """);
            Console.Write("Select: ");
            bool isInt = int.TryParse(Console.ReadLine(), out int option);
            if (!isInt || (option < 1 || option > 7))
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
                    ChargeBattery();
                    break;

                case 7:
                    return;

                default:
                    Console.WriteLine("Invalid option. Please choice again!");
                    break;
            }
            Console.WriteLine();
        } while (true);

    }

    void AddDrone()
    {
        Console.WriteLine("Insert the type and name of the drone.");
        Console.Write("Type (Survey/Delivery/Racing): ");
        string type = Console.ReadLine()!.Trim().ToLower();
        Console.Write("Name: ");
        string name = Console.ReadLine()!.Trim();
        switch (type)
        {
            case "delivery":
                Console.Write("Insert the total carrying capacity of the drone: ");
                bool isInt = int.TryParse(Console.ReadLine(), out int value);
                if (!isInt || value <= 0)
                {
                    Console.WriteLine("Input Invalid");
                    return;
                }
                Drones.Add(DroneFactory.CreateDrone(new DroneSpecs { Type = DroneType.Delivery, Id = ++currentId, Name = name, BatteryPercent = 100, CapacityKg = value }));
                break;

            case "raceing":
                Drones.Add(DroneFactory.CreateDrone(new DroneSpecs { Type = DroneType.Racing, BatteryPercent = 100, Name = name, Id = ++currentId }));
                break;

            case "survey":
                Drones.Add(DroneFactory.CreateDrone(new DroneSpecs { Type = DroneType.Survey, BatteryPercent = 100, Name = name, Id = ++currentId }));

                break;

            default:
                Console.WriteLine("Invalid input");
                return;
        }
        Console.WriteLine($"Added {type} drone with id {currentId} and name {name}");
    }

    void CheckAllDrones()
    {
        foreach (var drone in Drones)
        {
            string aux = drone.RunSelfTest() ? "does" : "doesn't";
            Console.WriteLine($"Drone {drone.Name} {aux} have enough battery to start the flight. Current battery percentage: {drone.BatteryPercent} ");
        }
    }

    void TakeOffLandDrone()
    {
        Console.Write("Insert the ID of the drone: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid Input.");
            return;
        }
        var drone = Drones.Find(x => x.Id == id);
        if (drone == null)
        {
            Console.WriteLine("Drone not found");
            return;
        }
        if (drone.IsAirborne)
        {
            Console.WriteLine($"Drone {drone.Name} is Landing....");
            drone.Land();
            Thread.Sleep(500);
            Console.WriteLine("Drone landed safely");
            return;
        }
        Console.WriteLine($"Drone {drone.Name} is preparing to launch....");
        drone.TakeOff();
        Thread.Sleep(500);
        Console.WriteLine("Drone is in air");
        return;
    }

    void SetWaypoint()
    {
        Console.Write("Enter the id of the drone: ");
        bool isInt = int.TryParse(Console.ReadLine(), out int id);
        if (!isInt)
        {
            Console.WriteLine("Id must be an integer.");
            return;
        }
        var drone = Drones.Find(x => x.Id == id);
        if (drone == null)
        {
            Console.WriteLine("Drone not found");
            return;
        }
        if (drone is not INavigable)
        {
            Console.WriteLine("A waypoint can't be set for this kind of drone");
            return;
        }
        Console.WriteLine("Insert the coordonates of the waypoint (must be numbers): ");
        Console.Write("Longitude: ");
        if (!double.TryParse(Console.ReadLine(), out double longitude))
        {
            Console.WriteLine("Invalid input.");
            return;
        }
        Console.WriteLine("Latitude: ");
        if (!double.TryParse(Console.ReadLine(), out double latitude))
        {
            Console.WriteLine("Invalid input.");
            return;
        }

        ((INavigable)drone).SetWaypoint(latitude, longitude);
        Console.WriteLine("The waypoint was set");
    }

    void ChargeBattery()
    {
        Console.Write("Enter the id of the drone: ");
        bool isInt = int.TryParse(Console.ReadLine(), out int id);
        if (!isInt)
        {
            Console.WriteLine("Id must be an integer.");
            return;
        }
        var drone = Drones.Find(x => x.Id == id);
        if (drone == null)
        {
            Console.WriteLine("Drone not found");
            return;
        }
        drone.BatteryPercent = 100;
        Console.WriteLine($"The battery for drone {drone.Name} was charged to {drone.BatteryPercent}.");
    }

    void ListDrones()
    {
        foreach (var drone in Drones)
        {
            string specialAttribute = string.Format("{0,-18}", drone switch
            {
                DeliveryDrone dd => $"Total capacity: {dd.CapacityKg.ToString()}",
                SurveyDrone sd => $"Photos taken: {sd.PhotoCount.ToString()}",
                _ => ""
            });
            string waypoint = drone is INavigable ? $"Current waypoint {((INavigable)drone).CurrentWaypoint.ToString()}" : "";

            string id = string.Format("{0, -3}", drone.Id.ToString());
            string name = string.Format("{0, -10}", drone.Name);
            string batteryPercentage = string.Format("{0,-3}", drone.BatteryPercent);
            string type = string.Format("{0,-13}", drone.GetType().Name.ToString());
            Console.WriteLine($"{id} | {name} | {type} | {batteryPercentage} | {specialAttribute} | {waypoint}");
        }
    }

}
