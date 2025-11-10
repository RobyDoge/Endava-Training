namespace Cafe.Domain.Beverages.Decorators;

public class MilkDecorator : BeverageDecorator
{
    public MilkDecorator(IBeverage beverage) : base(beverage)
    {
    }

    public override string Name => $"{_beverage.Name} + Milk";

    public override decimal Cost() => _beverage.Cost() + 0.4m;

    public override string Description() => $"{_beverage.Description()} + Milk ($0.40)";
}