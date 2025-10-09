namespace Basics.Base;
public abstract class Vehicle
{
    public required string Plate { get; set; }
    public required int Capacity { get; set; }   
    public required decimal BaseFareKm { get; set; }
    public virtual decimal CalcCost (decimal distanceKm) => BaseFareKm * distanceKm;
}
