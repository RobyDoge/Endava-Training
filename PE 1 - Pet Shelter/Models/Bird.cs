using PetShelter.Interfaces;

namespace PetShelter.Models;

internal class Bird: Animal, IFeedable, IFlyable
{
    public double WingSpanCm { get; set; }

    public void Feed()
    {
        Console.WriteLine($"Birdo {Name} Feed");
    }

    public void Fly()
    {
        Console.WriteLine($"Birdo {Name} has a {WingSpanCm} Wing Span");
    }
    public override void Speak()
    {
        Console.WriteLine($"{Name}: Chunchun");
    }
    public override decimal DayliyCareCost() => base.DayliyCareCost() + 1.0m;

}
