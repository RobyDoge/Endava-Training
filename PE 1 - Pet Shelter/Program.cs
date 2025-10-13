using PetShelter.Interfaces;
using PetShelter.Models;

List<Animal> animals = new()
{
    new Dog { Name = "Doggo", Age = 3, IsTrained = true },
    new Cat { Name = "Catto", Age = 2, IsIndoor = false },
    new Bird { Name = "Birdo", Age = 1, WingSpanCm = 25.5 }
};