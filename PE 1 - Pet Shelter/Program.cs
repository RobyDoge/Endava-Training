using PetShelter.Interfaces;
using PetShelter.Models;

List<Animal> animals = new()
{
    new Dog { Id = 1, Name = "Bella", Age = 3, IsTrained = true },
    new Cat { Id = 2, Name = "Oreo", Age = 2, IsIndoor = false },
    new Bird { Id = 3, Name = "Pipsqueak", Age = 1, WingSpanCm = 25.5 }
};

int lastId = 3;

do
{
    Console.WriteLine("1. Add Dog");
    Console.WriteLine("2. Add Cat");
    Console.WriteLine("3. Add Bird");
    Console.WriteLine("4. List Animals");
    Console.WriteLine("5. Feed All");
    Console.WriteLine("6. Speak All");
    Console.WriteLine("7. Adopt (by id)");
    Console.WriteLine("8. Fly All");
    Console.WriteLine("0. Exit");

    string input = Console.ReadLine() ?? "0";
    int option = int.TryParse(input, out int result) ? result : 0;
    if (option == 0) break;
    if (option < 0 || option > 8)
    {
        Console.WriteLine("Invalid option.");
        continue;
    }
    switch (option)
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
        case 7 :
            AdoptById();
            break;
        case 8:
            FlyAll();
            break;
        default:
            break;
    }

}while (true);

void FlyAll()
{
    foreach (var animal in animals)
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
    string input = Console.ReadLine() ?? "";
    int id = int.TryParse(input, out int idResult) ? idResult : -1;
    if (id <= 0)
    {
        Console.WriteLine("Invalid Id.");
        return;
    }
    var animal = animals.Find(a => a.Id == id);
    if(animal == null)
    {
        Console.WriteLine("Animal not Found");
        return;
    }
    animals.Remove(animal);
    Console.WriteLine($"You adopted {animal.Name}. Take good care of it!");
}

void SpeakAll()
{
    foreach (var animals in animals)
    {
        animals?.Speak();
    }
}

void FeedAll()
{
    int feedCound = 0;
    foreach (var animal in animals)
    {
        if (animal is IFeedable feedable)
        {
            feedable.Feed();
            feedCound++;
        }
    }
}

void ListAnimals()
{
    Console.WriteLine("ID  | Name       | Age | Extra              | Cost");
    Console.WriteLine("--------------------------------------------------");
    foreach (var animal in animals)
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
    Console.WriteLine("Insert the Name, Age, WingSpanCm(number) of the bird separated by blank spaces");
    string input = Console.ReadLine() ?? "";
    string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    if (parts.Length != 3)
    {
        Console.WriteLine("Invalid input.");
        return;
    }
    string name = parts[0];
    int age = int.TryParse(parts[1], out int ageResult) ? ageResult : -1;
    if (age <= 0)
    {
        Console.WriteLine("Invalid age.");
        return;
    }
    double wingSpanCm = double.TryParse(parts[2], out double wingSpanCmResult) ? wingSpanCmResult : -1;
    if (wingSpanCm <= 0)
    {
        Console.WriteLine("Invalid WingSpanCm.");
        return;
    }
    animals.Add(new Bird { Id = ++lastId, Name = name, Age = age, WingSpanCm = wingSpanCm, IntakeDate = DateTime.Now });
}

void AddCat()
{
    Console.WriteLine("Insert the Name, Age, IsIndoor(y/n) of the cat separated by blank spaces");
    string input = Console.ReadLine() ?? "";
    string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    if (parts.Length != 3)
    {
        Console.WriteLine("Invalid input.");
        return;
    }
    string name = parts[0];
    int age = int.TryParse(parts[1], out int ageResult) ? ageResult : -1;
    if (age <= 0)
    {
        Console.WriteLine("Invalid age.");
        return;
    }
    if (parts[2].ToLower() != "y" && parts[2].ToLower() != "n")
    {
        Console.WriteLine("Invalid IsTrained.");
        return;
    }
    bool isIndoor = parts[2].ToLower() == "y";
    animals.Add(new Cat { Id = ++lastId, Name = name, Age = age, IsIndoor = isIndoor, IntakeDate = DateTime.Now });

}

void AddDog()
{
    Console.WriteLine("Insert the Name, Age, IsTrained(y/n) of the dog separated by blank spaces");
    string input = Console.ReadLine() ?? "";
    string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    if (parts.Length != 3)
    {
        Console.WriteLine("Invalid input.");
        return;
    }
    string name = parts[0];
    int age = int.TryParse(parts[1], out int ageResult) ? ageResult : -1;
    if (age <= 0)
    {
        Console.WriteLine("Invalid age.");
        return;
    }
    if (parts[2].ToLower() != "y" && parts[2].ToLower() != "n")
    {
        Console.WriteLine("Invalid IsTrained.");
        return;
    }
    bool isTrained = parts[2].ToLower() == "y";
    
    animals.Add(new Dog { Id = ++lastId, Name = name, Age = age, IsTrained = isTrained, IntakeDate = DateTime.Now });


}