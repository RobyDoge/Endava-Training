namespace Cafe.Domain.Beverages.Decorators;

public class ExtraShotDecorator : BeverageDecorator
{
    public ExtraShotDecorator(IBeverage beverage) : base(beverage)
    {
    }

    public override string Name => $"{_beverage.Name} + Extra Shot";

    public override decimal Cost() => _beverage.Cost() + 0.8m;

    public override string Description() => $"{_beverage.Description()} + ExtraShot($0.80)";
}