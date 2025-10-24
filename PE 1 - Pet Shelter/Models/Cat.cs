using PetShelter.Interfaces;

namespace PetShelter.Models;

internal class Cat: Animal, IFeedable
{
    public bool IsIndoor { get; set; }
    
    public void Feed()
    {
        Console.WriteLine($"Catto {Name} Feed");
    }
    public override void Speak()
    {
        Console.WriteLine($"{Name}: Nyan");
    }
    public override decimal DayliyCareCost() => base.DayliyCareCost() + 2.0m;
}
