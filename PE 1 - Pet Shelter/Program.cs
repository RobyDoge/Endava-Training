using PetShelter.Interfaces;
using PetShelter.Models;

List<Animal> animals = new()
{
    new Dog { Id = 1, Name = "Doggo", Age = 3, IsTrained = true },
    new Cat { Id = 2, Name = "Catto", Age = 2, IsIndoor = false },
    new Bird { Id = 3, Name = "Birdo", Age = 1, WingSpanCm = 25.5 }
};

uint lastId = 3;

do
{
    Console.WriteLine("1. Add Dog");
    Console.WriteLine("2. Add Cat");
    Console.WriteLine("3. Add Bird");
    Console.WriteLine("4. List Animals");
    Console.WriteLine("5. Feed All");
    Console.WriteLine("6. Speak All");
    Console.WriteLine("7. Adopt (by id)");
    Console.WriteLine("0. Exit");

    string input = Console.ReadLine() ?? "0";
    int option = int.TryParse(input, out int result) ? result : 0;
    if (option == 0) break;
    if (option < 0 || option > 7)
    {
        Console.WriteLine("Invalid option");
        continue;
    }

}while (true);

