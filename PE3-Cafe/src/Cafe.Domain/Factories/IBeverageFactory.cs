using Cafe.Domain.Beverages;
using Cafe.Domain.Beverages.Decorators;
using Cafe.Domain.Result;

namespace Cafe.Domain.Factories;

public interface IBeverageFactory
{
    Result<IBeverage> Create(BeverageType beverageType);

    Result<IBeverage> Create(DecoratorType decoratorType, IBeverage beverage, params List<string?> additionalInfo);
}