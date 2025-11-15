namespace Cafe.Domain.Beverages.Decorators;

public abstract class BeverageDecorator : IBeverage
{
    protected IBeverage _beverage;

    protected BeverageDecorator(IBeverage beverage)
    {
        _beverage = beverage;
    }

    public virtual string Name => _beverage.Name;

    public virtual decimal Cost() => _beverage.Cost();

    public virtual string Description() => _beverage.Description();
}