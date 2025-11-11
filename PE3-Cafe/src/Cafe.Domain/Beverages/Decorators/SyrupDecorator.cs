namespace Cafe.Domain.Beverages.Decorators;

public class SyrupDecorator : BeverageDecorator
{
    private string _flavour;

    private SyrupDecorator(IBeverage beverage, string flavour) : base(beverage)
    {
        _flavour = flavour.ToLower();
    }

    public override string Name => $"{_beverage.Name} + {_flavour} Syrup";

    public override decimal Cost() => _beverage.Cost() + 0.5m;

    public override string Description() => $"{_beverage.Description()} {_flavour} Syrup ($0.50)";
}