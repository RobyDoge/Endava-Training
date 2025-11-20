using Cafe.Domain.Beverages;
using Cafe.Domain.Beverages.Decorators;

namespace Cafe.Tests;

public class DecoratorTests
{
    [Fact]
    public void Decorator_AddMilkAndExtrashotToEspresso()
    {
        IBeverage beverage = new Espresso();

        beverage = new MilkDecorator(beverage);
        beverage = new ExtraShotDecorator(beverage);

        Assert.Equal(3.70m, beverage.Cost());
        Assert.Contains("Milk", beverage.Description());
        Assert.Contains("ExtraShot", beverage.Description());
    }
}