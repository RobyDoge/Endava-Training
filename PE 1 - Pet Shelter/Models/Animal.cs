namespace PetShelter.Models;

internal abstract class Animal
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public DateTime IntakeDate { get;set; }

    public abstract void Speak();
    public virtual decimal DayliyCareCost() => 5.0m;

}
