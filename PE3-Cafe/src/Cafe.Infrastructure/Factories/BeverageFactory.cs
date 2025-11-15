using Cafe.Domain.Beverages;
using Cafe.Domain.Beverages.Decorators;
using Cafe.Domain.Factories;
using Cafe.Domain.Result;

namespace Cafe.Infrastructure.Factories;

public class BeverageFactory : IBeverageFactory
{
    public Result<IBeverage> Create(DecoratorType decoratorType, IBeverage beverage, params List<string?> additionalInfo)
    {
        return decoratorType switch
        {
            DecoratorType.Milk => new MilkDecorator(beverage),
            DecoratorType.Syrup => new SyrupDecorator(beverage, additionalInfo!.FirstOrDefault() ?? string.Empty),
            DecoratorType.ExtraShot => new ExtraShotDecorator(beverage),
            _ => Result.Failure<IBeverage>(Error.InvalidDecoratorType)
        };
    }

    public Result<IBeverage> Create(BeverageType beverageType)
    {
        return beverageType switch
        {
            BeverageType.Espresso => new Espresso(),
            BeverageType.Tea => new Tea(),
            BeverageType.HotChocolate => new HotChocolate(),
            _ => Result.Failure<IBeverage>(Error.InvalidBeverageType)
        };
    }
}