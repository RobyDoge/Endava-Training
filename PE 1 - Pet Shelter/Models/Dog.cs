using PetShelter.Interfaces;

namespace PetShelter.Models;

internal class Dog : Animal, IFeedable
{
    public bool IsTrained { get; set; }

    public void Feed()
    {
        Console.WriteLine("Doggo Feed");
    }

    public override void Speak()
    {
        Console.WriteLine("Wan Wan");
    }

    public override decimal DayliyCareCost() => base.DayliyCareCost() + 3.0m;
}
