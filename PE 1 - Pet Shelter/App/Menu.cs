using PetShelter.Interfaces;
using PetShelter.Models;
using System.Reflection.Metadata.Ecma335;

namespace PetShelter.App;

public class Menu
{
    private List<Animal> Animals { get; set; } = [];
    private int CurrentId { set; get; } = 0;
    
    public void Run()
    {
        do
        {
            Console.WriteLine();
            DisplayMenu();
            if (!int.TryParse(Console.ReadLine(), out var option)) { InvalidInput("number"); continue; }
            switch(option)
            {
                case 1:
                    AddDog();
                    break;
                case 2:
                    AddCat();
                    break;
                case 3:
                    AddBird();
                    break;
                case 4:
                    ListAnimals();
                    break;
                case 5:
                    FeedAll();
                    break;
                case 6:
                    SpeakAll();
                    break;
                case 7:
                    AdoptById();
                    break;
                case 8:
                    FlyAll();
                    break;
                case 0:
                    return;
                default:
                    break;
            }


        } while (true);
    }

    #region ConsoleDisplay
    private void DisplayMenu()
    {
        var menu = """
            1. Add Dog
            2. Add Cat
            3. Add Bird
            4. List Animals
            5. Feed All
            6. Speak All
            7. Adopt (by id)
            8. Fly All
            0. Exit
            """;
        Console.WriteLine(menu);
    }
    #endregion

    #region ConsoleErrors
    private void InvalidInput(string type)
    {
        Console.WriteLine($"Input invalid. It must be {type}");
    }
    private void AnimalNotFound()
    {
        Console.WriteLine("Animal not found");
    }
    #endregion

    void FlyAll()
    {
        foreach (var animal in Animals)
        {
            if (animal is IFlyable flyable)
            {
                flyable.Fly();
            }
        }
    }
    void AdoptById()
    {
        Console.WriteLine("Please insert the Id of the animal you want to adopt");
        if(!int.TryParse(Console.ReadLine(), out var id)) { InvalidInput("number"); return; }

        var animal = Animals.Find(a => a.Id == id);
        if (animal == null) { AnimalNotFound(); return; }
        
        Animals.Remove(animal);
        Console.WriteLine($"You adopted {animal.Name}. Take good care of them!");
    }
    void SpeakAll()
    {
        foreach (var Animals in Animals)
        {
            Animals?.Speak();
        }
    }
    void FeedAll()
    {
        int feedCound = 0;
        foreach (var animal in Animals)
        {
            if (animal is IFeedable feedable)
            {
                feedable.Feed();
                feedCound++;
            }
        }
        Console.WriteLine($"{feedCound} animals were fed.");
    }
    void ListAnimals()
    {
        Console.WriteLine("ID  | Name       | Age | Extra              | Cost");
        Console.WriteLine("--------------------------------------------------");
        foreach (var animal in Animals)
        {
            string id = string.Format("{0,-3}", animal.Id);
            string name = string.Format("{0,-10}", animal.Name);
            string age = string.Format("{0,-3}", animal.Age);
            string extra = animal switch
            {
                Dog dog => $"IsTrained: {dog.IsTrained}",
                Cat cat => $"IsIndoor: {cat.IsIndoor}",
                Bird bird => $"WingSpanCm: {bird.WingSpanCm}",
                _ => ""
            };
            extra = string.Format("{0,-18}", extra);
            string dailyCareCost = string.Format("{0,-5}", animal.DayliyCareCost());
            Console.WriteLine($"{id} | {name} | {age} | {extra} | {dailyCareCost}");
        }
    }
    void AddBird()
    {
        Console.Write("Insert the Name: ");
        string name = Console.ReadLine();

        Console.Write("Insert the age: ");
        if(!int.TryParse(Console.ReadLine(), out var age) && age > 0)
        {
            InvalidInput("number greater than 0"); 
            return; 
        }
        
        Console.Write("Enter the wing span in cm: ");
        if (!double.TryParse(Console.ReadLine(), out var wingSpanCm) && wingSpanCm > 0)
        {
            InvalidInput("double greater than 0");
            return;
        }

        Animals.Add(new Bird { Id = ++CurrentId, Name = name, Age = age, WingSpanCm = wingSpanCm, IntakeDate = DateTime.Now });
    }
    void AddCat()
    {
        Console.Write("Insert the Name: ");
        string name = Console.ReadLine();

        Console.Write("Insert the age: ");
        if (!int.TryParse(Console.ReadLine(), out var age) && age > 0)
        {
            InvalidInput("number greater than 0");
            return;
        }

        Console.Write("Is the cat for indoors? (y/n): ");
        string input = Console.ReadLine();
        if (input.ToLower() != "y" && input.ToLower() != "n")
        {
            InvalidInput("y or n");
            return;
        }

        bool isIndoor = input == "y";
        Animals.Add(new Cat { Id = ++CurrentId, Name = name, Age = age, IsIndoor = isIndoor, IntakeDate = DateTime.Now });
    }
    void AddDog()
    {
        Console.Write("Insert the Name: ");
        string name = Console.ReadLine();
        
        Console.Write("Insert the age: ");
        if (!int.TryParse(Console.ReadLine(), out var age) && age > 0)
        {
            InvalidInput("number greater than 0");
            return;
        }
        
        Console.Write("Is the dog for trained? (y/n): ");
        string input = Console.ReadLine();
        if (input.ToLower() != "y" && input.ToLower() != "n")
        {
            InvalidInput("y or n");
            return;
        }

        bool isTrained = input.ToLower() == "y";
        Animals.Add(new Dog { Id = ++CurrentId, Name = name, Age = age, IsTrained = isTrained, IntakeDate = DateTime.Now });
    }
}
